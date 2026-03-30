import 'package:amsfront/app/di/riverpod_di/provider.dart';
import 'package:amsfront/app/enums/userStatus.dart';
import 'package:amsfront/features/SuperAdmin/data/model/status_request.dart';
import 'package:amsfront/features/SuperAdmin/data/model/user_model.dart';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:go_router/go_router.dart';


class UserManagementPage extends ConsumerStatefulWidget {
  const UserManagementPage({super.key});

  @override
  ConsumerState<UserManagementPage> createState() => _UserManagementPageState();
}

class _UserManagementPageState extends ConsumerState<UserManagementPage> {
  final TextEditingController _searchController = TextEditingController();

  @override
  void initState() {
    super.initState();
    // Fetch users when the page initializes
    WidgetsBinding.instance.addPostFrameCallback((_) {
      ref.read(userManagProvide.notifier).fetchUsers();
    });
  }

  @override
  void dispose() {
    _searchController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    final userState = ref.watch(userManagProvide);
    final pendingUsers = userState.filteredUsers.where((u) => u.status == UserStatus.Pending).toList();
    final currentUsers = userState.filteredUsers.where((u) => u.status != UserStatus.Pending).toList();

    return Scaffold(
      backgroundColor: const Color(0xFF121721), // Matches CSS background
      appBar: AppBar(
        backgroundColor: const Color(0xFF121721),
        elevation: 0,
        leading: IconButton(
          icon: const Icon(Icons.arrow_back_ios_new, color: Colors.white),
          onPressed: () => context.pop(),
        ),
        title: const Text('Users', style: TextStyle(color: Colors.white, fontWeight: FontWeight.w700, fontSize: 18)),
        centerTitle: true,
      ),
      body: Column(
        children: [
          // Search and Filter
          Padding(
            padding: const EdgeInsets.fromLTRB(16, 8, 16, 12),
            child: Column(
              children: [
                TextField(
                  controller: _searchController,
                  style: const TextStyle(color: Colors.white),
                  decoration: InputDecoration(
                    hintText: 'Search users...',
                    hintStyle: const TextStyle(color: Color(0xFF94A6C7)),
                    prefixIcon: const Icon(Icons.search, color: Color(0xFF94A6C7)),
                    filled: true,
                    fillColor: const Color(0xFF243347),
                    border: OutlineInputBorder(
                      borderRadius: BorderRadius.circular(8),
                      borderSide: BorderSide.none,
                    ),
                    focusedBorder: OutlineInputBorder(
                      borderRadius: BorderRadius.circular(8),
                      borderSide: const BorderSide(color: Color(0xFF2563EB)),
                    ),
                    contentPadding: const EdgeInsets.symmetric(vertical: 0),
                  ),
                  onChanged: (value) {
                    ref.read(userManagProvide.notifier).searchUsers(value);
                  },
                ),
                const SizedBox(height: 12),
                Row(
                  children: const [
                    _FilterChip(label: 'Role'),
                    SizedBox(width: 12),
                    _FilterChip(label: 'Status'),
                  ],
                ),
              ],
            ),
          ),
          // Lists
          Expanded(
            child: userState.isLoading
                ? const Center(child: CircularProgressIndicator(color: Colors.white))
                : userState.error != null
                    ? Center(child: Text('Error: ${userState.error}', style: const TextStyle(color: Colors.red)))
                    : userState.filteredUsers.isEmpty
                        ? const Center(child: Text('No users found', style: TextStyle(color: Colors.white70)))
                        : SingleChildScrollView(
                            child: Column(
                              crossAxisAlignment: CrossAxisAlignment.start,
                              children: [
                                if (pendingUsers.isNotEmpty) ...[
                                  const _SectionHeader(title: 'Requests'),
                                  ...pendingUsers.map((user) => _UserRequestCard(user: user)).toList(),
                                ],
                                if (currentUsers.isNotEmpty) ...[
                                  const _SectionHeader(title: 'Current Users'),
                                  ...currentUsers.map((user) => _CurrentUserCard(user: user)).toList(),
                                ],
                              ],
                            ),
                          ),
          ),
        ],
      ),
    );
  }
}

class _FilterChip extends StatelessWidget {
  final String label;
  const _FilterChip({required this.label});

  @override
  Widget build(BuildContext context) {
    return Container(
      padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 6),
      decoration: BoxDecoration(
        color: const Color(0xFF243347),
        borderRadius: BorderRadius.circular(8),
      ),
      child: Row(
        children: [
          Text(label, style: const TextStyle(color: Colors.white, fontSize: 14, fontWeight: FontWeight.w500)),
          const SizedBox(width: 8),
          const Icon(Icons.keyboard_arrow_down, color: Colors.white, size: 20),
        ],
      ),
    );
  }
}

class _SectionHeader extends StatelessWidget {
  final String title;
  const _SectionHeader({required this.title});

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.fromLTRB(16, 20, 16, 12),
      child: Text(
        title,
        style: const TextStyle(
          color: Colors.white,
          fontSize: 22,
          fontWeight: FontWeight.w700,
        ),
      ),
    );
  }
}

class _UserRequestCard extends ConsumerWidget {
  final UserResponse user;
  const _UserRequestCard({required this.user});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final name = user.fullName ;
    final role = user.role ;

    return Container(
      padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 12),
      child: Row(
        children: [
          const CircleAvatar(
            radius: 35,
            backgroundColor: Color(0xFFC8F1FF),
            // In a real app, you'd use a NetworkImage here
          ),
          const SizedBox(width: 16),
          Expanded(
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                Text(name, style: const TextStyle(color: Colors.white, fontSize: 16, fontWeight: FontWeight.w500)),
                const SizedBox(height: 4),
                Text('Role: $role', style: const TextStyle(color: Color(0xFF94A6C7), fontSize: 14)),
              ],
            ),
          ),
          const SizedBox(width: 16),
          Column(
            children: [
              _ActionButton(
                text: 'Accept',
                color: const Color(0xFF22C55E),
                backgroundColor: const Color(0xFF22C55E).withOpacity(0.2),
                onTap: () => ref.read(userManagProvide.notifier).approveUser(
                  StatusRequest(userID: user.id, status: UserStatus.Active),
                ),
              ),
              const SizedBox(height: 6),
              _ActionButton(
                text: 'Decline',
                color: const Color(0xFFEF4444),
                backgroundColor: const Color(0xFFEF4444).withOpacity(0.2),
                onTap: () => ref.read(userManagProvide.notifier).rejectUser(
                  StatusRequest(userID: user.id, status: UserStatus.Inactive),
                ),
              ),
            ],
          )
        ],
      ),
    );
  }
}

class _CurrentUserCard extends StatelessWidget {
  final UserResponse user;
  const _CurrentUserCard({required this.user});

  @override
  Widget build(BuildContext context) {
    final name = user.username ;
    final role = user.role;
    final status = user.status;

    return Container(
      padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 8),
      child: Row(
        children: [
          const CircleAvatar(
            radius: 28,
            backgroundColor: Color(0xFFC8F1FF),
          ),
          const SizedBox(width: 16),
          Expanded(
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                Text(name, style: const TextStyle(color: Colors.white, fontSize: 16, fontWeight: FontWeight.w500)),
                const SizedBox(height: 4),
                Text(role, style: const TextStyle(color: Color(0xFF94A6C7), fontSize: 14)),
              ],
            ),
          ),
          Text(
            status.name,
            style: TextStyle(
              color: status == UserStatus.Active ? Colors.green : Colors.red,
              fontWeight: FontWeight.w500,
            ),
          ),
        ],
      ),
    );
  }
}

class _ActionButton extends StatelessWidget {
  final String text;
  final Color color;
  final Color backgroundColor;
  final VoidCallback onTap;

  const _ActionButton({
    required this.text,
    required this.color,
    required this.backgroundColor,
    required this.onTap,
  });

  @override
  Widget build(BuildContext context) {
    return InkWell(
      onTap: onTap,
      child: Container(
        width: 84,
        height: 32,
        decoration: BoxDecoration(
          color: backgroundColor,
          borderRadius: BorderRadius.circular(8),
        ),
        child: Center(
          child: Text(
            text,
            style: TextStyle(color: color, fontWeight: FontWeight.w500, fontSize: 14),
          ),
        ),
      ),
    );
  }
}