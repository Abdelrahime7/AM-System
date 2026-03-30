

import 'package:amsfront/features/SuperAdmin/data/model/user_model.dart';

class UserManagState{

  final bool isLoading;
  final String? error;
  final List<UserResponse> users;
  final List<UserResponse> filteredUsers;
  final String searchQuery;

  UserManagState({
    this.isLoading = false,
    this.error,
    this.users = const [],
    this.filteredUsers = const [],
    this.searchQuery = "",
  });

  UserManagState copyWith({
    bool? isLoading,
    String? error,
    List<UserResponse>? users,
    List<UserResponse>? filteredUsers,
    String? searchQuery,
  }) {
    return UserManagState(
      isLoading: isLoading ?? this.isLoading,
      error: error ?? this.error,
      users: users ?? this.users,
      filteredUsers: filteredUsers ?? this.filteredUsers,
      searchQuery: searchQuery ?? this.searchQuery,
    );
  }

}