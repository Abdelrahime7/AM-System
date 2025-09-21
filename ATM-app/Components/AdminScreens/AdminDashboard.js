import React from 'react';
import {
  View,
  Text,
  StyleSheet,
  TouchableOpacity,
  ScrollView,
  SafeAreaView,
} from 'react-native';
import { Ionicons } from '@expo/vector-icons';

const AdminDashboard = () => {
  const statsData = [
    { title: 'Total Sales', value: '1,250' },
    { title: 'Active Marketers', value: '320' },
    { title: 'Pending Orders', value: '75' },
    { title: 'Total Revenue', value: '$15,000' },
  ];

  const quickActions = [
    { title: 'Manage Users', icon: 'people-outline' },
    { title: 'Manage Products', icon: 'cube-outline' },
    { title: 'Manage Finance', icon: 'card-outline' },
    { title: 'View Reports', icon: 'bar-chart-outline' },
  ];

  const recentActivity = [
    {
      type: 'New User',
      description: "User 'Omar Hassan' registered as an affiliate.",
      icon: 'person-add-outline',
      color: '#3B82F6',
    },
    {
      type: 'Withdrawal Request',
      description: "Request of $500 by 'Layla Ali' pending.",
      icon: 'card-outline',
      color: '#22C55E',
    },
    {
      type: 'Product Added',
      description: "Product 'Smartphone X' added to the catalog.",
      icon: 'cube-outline',
      color: '#A855F7',
    },
  ];

  return (
    <SafeAreaView style={styles.container}>
      {/* Header */}
      <View style={styles.header}>
        <View style={styles.headerContent}>
          <Text style={styles.headerTitle}>Dashboard</Text>
          <TouchableOpacity style={styles.menuButton}>
            <Ionicons name="menu-outline" size={24} color="#FFFFFF" />
          </TouchableOpacity>
        </View>
      </View>

      <ScrollView showsVerticalScrollIndicator={false}>
        {/* Welcome Section */}
        <View style={styles.welcomeSection}>
          <Text style={styles.welcomeText}>Welcome back, Omar!</Text>
        </View>

        {/* Stats Grid */}
        <View style={styles.statsContainer}>
          {statsData.map((stat, index) => (
            <View key={index} style={styles.statCard}>
              <Text style={styles.statTitle}>{stat.title}</Text>
              <Text style={styles.statValue}>{stat.value}</Text>
            </View>
          ))}
        </View>

        {/* Quick Actions */}
        <View style={styles.sectionHeader}>
          <Text style={styles.sectionTitle}>Quick Actions</Text>
        </View>

        <View style={styles.quickActionsContainer}>
          <View style={styles.actionRow}>
            {quickActions.slice(0, 2).map((action, index) => (
              <TouchableOpacity key={index} style={styles.actionCard}>
                <Ionicons name={action.icon} size={24} color="#FFFFFF" />
                <Text style={styles.actionTitle}>{action.title}</Text>
              </TouchableOpacity>
            ))}
          </View>
          <View style={styles.actionRow}>
            {quickActions.slice(2, 4).map((action, index) => (
              <TouchableOpacity key={index} style={styles.actionCard}>
                <Ionicons name={action.icon} size={24} color="#FFFFFF" />
                <Text style={styles.actionTitle}>{action.title}</Text>
              </TouchableOpacity>
            ))}
          </View>
        </View>

        {/* Recent Activity */}
        <View style={styles.sectionHeader}>
          <Text style={styles.sectionTitle}>Recent Activity</Text>
        </View>

        <View style={styles.activityContainer}>
          {recentActivity.map((activity, index) => (
            <View key={index} style={styles.activityItem}>
              <View style={[styles.activityIcon, { backgroundColor: `${activity.color}33` }]}>
                <Ionicons name={activity.icon} size={24} color="#FFFFFF" />
              </View>
              <View style={styles.activityContent}>
                <Text style={styles.activityType}>{activity.type}</Text>
                <Text style={styles.activityDescription}>{activity.description}</Text>
              </View>
            </View>
          ))}
        </View>
      </ScrollView>

      {/* Bottom Navigation */}
      <View style={styles.bottomNav}>
        <TouchableOpacity style={styles.navItem}>
          <Ionicons name="home" size={24} color="#FFFFFF" />
          <Text style={styles.navActiveText}>Home</Text>
        </TouchableOpacity>
        <TouchableOpacity style={styles.navItem}>
          <Ionicons name="people-outline" size={24} color="#FFFFFF66" />
          <Text style={styles.navText}>Users</Text>
        </TouchableOpacity>
        <TouchableOpacity style={styles.navItem}>
          <Ionicons name="cube-outline" size={24} color="#FFFFFF66" />
          <Text style={styles.navText}>Products</Text>
        </TouchableOpacity>
        <TouchableOpacity style={styles.navItem}>
          <Ionicons name="card-outline" size={24} color="#FFFFFF66" />
          <Text style={styles.navText}>Finance</Text>
        </TouchableOpacity>
        <TouchableOpacity style={styles.navItem}>
          <Ionicons name="bar-chart-outline" size={24} color="#FFFFFF66" />
          <Text style={styles.navText}>Analytics</Text>
        </TouchableOpacity>
      </View>
    </SafeAreaView>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#121721',
  },
  header: {
    backgroundColor: '#121721',
    paddingHorizontal: 16,
    paddingVertical: 16,
    paddingBottom: 8,
  },
  headerContent: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
  },
  headerTitle: {
    fontSize: 18,
    fontWeight: '700',
    color: '#FFFFFF',
    flex: 1,
    textAlign: 'center',
    marginLeft: 48,
  },
  menuButton: {
    width: 24,
    height: 24,
    justifyContent: 'center',
    alignItems: 'center',
  },
  welcomeSection: {
    paddingHorizontal: 16,
    paddingVertical: 20,
    paddingBottom: 8,
  },
  welcomeText: {
    fontSize: 24,
    fontWeight: '700',
    color: '#FFFFFF',
  },
  statsContainer: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    paddingHorizontal: 16,
    gap: 16,
    marginBottom: 20,
  },
  statCard: {
    backgroundColor: '#243047',
    borderRadius: 8,
    padding: 24,
    flex: 1,
    minWidth: 158,
    alignItems: 'center',
  },
  statTitle: {
    fontSize: 12,
    color: '#FFFFFF',
    textAlign: 'center',
    marginBottom: 4,
  },
  statValue: {
    fontSize: 24,
    fontWeight: '700',
    color: '#FFFFFF',
    textAlign: 'center',
  },
  sectionHeader: {
    paddingHorizontal: 16,
    paddingVertical: 20,
    paddingBottom: 12,
  },
  sectionTitle: {
    fontSize: 22,
    fontWeight: '700',
    color: '#FFFFFF',
  },
  quickActionsContainer: {
    paddingHorizontal: 16,
    gap: 12,
    marginBottom: 20,
  },
  actionRow: {
    flexDirection: 'row',
    gap: 12,
  },
  actionCard: {
    flex: 1,
    backgroundColor: '#1A2433',
    borderWidth: 1,
    borderColor: '#334566',
    borderRadius: 8,
    padding: 16,
    flexDirection: 'row',
    alignItems: 'center',
    gap: 12,
    minHeight: 90,
  },
  actionTitle: {
    fontSize: 16,
    fontWeight: '700',
    color: '#FFFFFF',
    flex: 1,
  },
  activityContainer: {
    paddingHorizontal: 16,
    marginBottom: 100,
  },
  activityItem: {
    flexDirection: 'row',
    alignItems: 'flex-start',
    marginBottom: 24,
    gap: 16,
  },
  activityIcon: {
    width: 40,
    height: 40,
    borderRadius: 20,
    justifyContent: 'center',
    alignItems: 'center',
  },
  activityContent: {
    flex: 1,
  },
  activityType: {
    fontSize: 14,
    fontWeight: '500',
    color: '#FFFFFF',
    marginBottom: 2,
  },
  activityDescription: {
    fontSize: 12,
    color: '#CBD5E1',
    lineHeight: 18,
  },
  bottomNav: {
    position: 'absolute',
    bottom: 0,
    left: 0,
    right: 0,
    backgroundColor: '#192433',
    flexDirection: 'row',
    paddingVertical: 9,
    paddingHorizontal: 16,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: -3 },
    shadowOpacity: 0.25,
    shadowRadius: 6,
    elevation: 5,
  },
  navItem: {
    flex: 1,
    alignItems: 'center',
    gap: 4,
  },
  navActiveText: {
    fontSize: 12,
    fontWeight: '500',
    color: '#FFFFFF',
  },
  navText: {
    fontSize: 12,
    fontWeight: '500',
    color: '#FFFFFF66',
  },
});

export default AdminDashboard;