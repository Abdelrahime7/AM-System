
import 'package:amsfront/features/SuperAdmin/data/model/status_request.dart';
import 'package:amsfront/features/SuperAdmin/data/model/user_model.dart';
import 'package:amsfront/features/SuperAdmin/data/repositories/dashboard_Repository.dart';
import 'package:amsfront/features/SuperAdmin/presentation/stateManagement/user_Manag_Stat.dart';
import 'package:flutter_riverpod/legacy.dart';

class UserManagNotifier extends StateNotifier<UserManagState> {
  final DashboardRepository _repository;

  UserManagNotifier(this._repository) : super(UserManagState());

  Future<void> fetchUsers() async {
    try {
      state = state.copyWith(isLoading: true, error: null);
      final users = await _repository.getUsers(); // Assuming this method exists in repository
      state = state.copyWith(
        isLoading: false,
        users: users,
        filteredUsers: users,
      );
    } catch (e) {
      state = state.copyWith(isLoading: false, error: e.toString());
    }
  }

  void searchUsers(String query) {
    final filtered = state.users.where((user) {
      final username = user.username.toString().toLowerCase();
      final fullName = user.fullName.toString().toLowerCase();
      final email = user.email.toString().toLowerCase();
      final searchLower = query.toLowerCase();
      return username.contains(searchLower) ||
          fullName.contains(searchLower) ||
          email.contains(searchLower);
    }).toList();

    state = state.copyWith(
      searchQuery: query,
      filteredUsers: filtered,
    );
  }

 Future<void> approveUser(StatusRequest status) async {
    await _changeUserStatus(status);
  }

  Future<void> rejectUser(StatusRequest status) async {
    await _changeUserStatus(status);
  }

  Future<void> _changeUserStatus(StatusRequest status) async {
    try {
      state = state.copyWith(isLoading: true, error: null);
      await _repository.changeUserStatus(status);
      
      final users = List<UserResponse>.from(state.users);
      final index = users.indexWhere((u) => u.id == status.userID);

      if (index != -1) {
        final updatedUser = users[index].copyWith(status: status.status);
        users.removeAt(index);
        users.insert(0, updatedUser); // Move the updated user to the top
        
        state = state.copyWith(isLoading: false, users: users);
        _reapplyFilter(users);
      }
    } catch (e) {
      state = state.copyWith(isLoading: false, error: e.toString());
    }
  }

  void _reapplyFilter(List<UserResponse> updatedUsers) {
    final filtered = updatedUsers.where((user) {
      final username = user.username.toString().toLowerCase();
      final fullName = user.fullName.toString().toLowerCase();
      final email = user.email.toString().toLowerCase();
      final searchLower = state.searchQuery.toLowerCase();
      return username.contains(searchLower) ||
          fullName.contains(searchLower) ||
          email.contains(searchLower);
    }).toList();

    state = state.copyWith(
      users: updatedUsers,
      filteredUsers: filtered,
    );
  }
}
