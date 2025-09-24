import React from 'react';
import {
  View,
  Text,
  StyleSheet,
  ScrollView,
  TouchableOpacity,
  SafeAreaView,
  StatusBar,
  Dimensions,
} from 'react-native';
import { Ionicons } from '@expo/vector-icons';

const { width } = Dimensions.get('window');

const AffiliateHomeScreen = () => {
  const orders = [
    {
      id: '12345',
      products: 'Product A, Product B',
      price: '$125.00',
      commission: '+$12.50',
      date: 'Oct 23, 2023',
      status: 'Delivered',
      statusColor: '#4ADE80',
      statusBg: 'rgba(34, 197, 94, 0.2)',
    },
    {
      id: '12346',
      products: 'Product C',
      price: '$75.00',
      commission: '+$7.50',
      date: 'Oct 22, 2023',
      status: 'En Route',
      statusColor: '#60A5FA',
      statusBg: 'rgba(59, 130, 246, 0.2)',
    },
    {
      id: '12347',
      products: 'Product D, Product E, Product F',
      price: '$250.00',
      commission: '+$25.00',
      date: 'Oct 21, 2023',
      status: 'Pending',
      statusColor: '#FACC15',
      statusBg: 'rgba(234, 179, 8, 0.2)',
    },
  ];

  const OrderCard = ({ order }) => (
    <View style={styles.orderCard}>
      <View style={styles.orderHeader}>
        <Text style={styles.orderNumber}>Order #{order.id}</Text>
        <View style={[styles.statusBadge, { backgroundColor: order.statusBg }]}>
          <Text style={[styles.statusText, { color: order.statusColor }]}>
            {order.status}
          </Text>
        </View>
      </View>
      
      <Text style={styles.products}>{order.products}</Text>
      
      <View style={styles.orderFooter}>
        <View style={styles.priceSection}>
          <Text style={styles.label}>Price</Text>
          <Text style={styles.price}>{order.price}</Text>
        </View>
        
        <View style={styles.commissionSection}>
          <Text style={styles.label}>Commission</Text>
          <Text style={styles.commission}>{order.commission}</Text>
        </View>
        
        <View style={styles.dateSection}>
          <Text style={styles.date}>{order.date}</Text>
        </View>
      </View>
    </View>
  );

  const NavItem = ({ title, iconName, active = false }) => (
    <TouchableOpacity style={styles.navItem}>
      <Ionicons 
        name={iconName} 
        size={24} 
        color={active ? '#FFFFFF' : 'rgba(255, 255, 255, 0.3)'} 
      />
      <Text style={[styles.navText, active && styles.navTextActive]}>
        {title}
      </Text>
    </TouchableOpacity>
  );

  return (
    <SafeAreaView style={styles.container}>
      <StatusBar barStyle="light-content" backgroundColor="#111722" />
      
      {/* Header */}
      <View style={styles.header}>
        <Text style={styles.headerTitle}>Dashboard</Text>
        <TouchableOpacity style={styles.headerButton}>
          <Ionicons name="menu" size={24} color="#6B7280" />
        </TouchableOpacity>
      </View>

      <ScrollView 
        style={styles.content} 
        showsVerticalScrollIndicator={false}
        contentContainerStyle={styles.scrollContent}
      >
        {/* Create New Order Button */}
        <TouchableOpacity style={styles.createOrderButton}>
          <Ionicons name="add-circle-outline" size={28} color="#FFFFFF" />
          <Text style={styles.createOrderText}>Create New Order</Text>
        </TouchableOpacity>

        {/* Details Section */}
        <View style={styles.detailsHeader}>
          <Text style={styles.detailsTitle}>Details</Text>
          <TouchableOpacity style={styles.filterButton}>
            <Text style={styles.filterText}>All Status</Text>
            <Ionicons name="chevron-down" size={16} color="#6B7280" />
          </TouchableOpacity>
        </View>

        {/* Orders List */}
        {orders.map((order, index) => (
          <OrderCard key={order.id} order={order} />
        ))}
      </ScrollView>

      {/* Manage Withdrawals Button */}
      <View style={styles.buttonContainer}>
        <TouchableOpacity style={styles.withdrawalButton}>
          <Ionicons name="wallet-outline" size={24} color="#FFFFFF" />
          <Text style={styles.withdrawalText}>Manage Withdrawals</Text>
        </TouchableOpacity>
      </View>

      {/* Bottom Navigation */}
      <View style={styles.navigation}>
        <NavItem title="Home" iconName="home-outline" active />
        <NavItem title="Products" iconName="cube-outline" />
        <NavItem title="Withdrawal" iconName="wallet-outline" />
        <NavItem title="Profile" iconName="person-outline" />
      </View>
    </SafeAreaView>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#111722',
  },
  header: {
    height: 72,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    paddingHorizontal: 16,
  },
  headerTitle: {
    fontSize: 20,
    fontWeight: '700',
    color: '#FFFFFF',
    letterSpacing: -0.27,
  },
  headerButton: {
    position: 'absolute',
    right: 16,
    width: 24,
    height: 24,
  },
  content: {
    flex: 1,
  },
  scrollContent: {
    paddingHorizontal: 16,
    paddingBottom: 20,
  },
  createOrderButton: {
    height: 101,
    backgroundColor: 'rgba(255, 255, 255, 0.05)',
    borderRadius: 12,
    alignItems: 'center',
    justifyContent: 'center',
    marginBottom: 20,
    shadowColor: '#000',
    shadowOffset: {
      width: 0,
      height: 4,
    },
    shadowOpacity: 0.1,
    shadowRadius: 6,
    elevation: 8,
  },
  createOrderText: {
    fontSize: 16,
    fontWeight: '500',
    color: '#FFFFFF',
    textAlign: 'center',
    marginTop: 8,
  },
  detailsHeader: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    marginBottom: 20,
  },
  detailsTitle: {
    fontSize: 17.5,
    fontWeight: '700',
    color: '#FFFFFF',
  },
  filterButton: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 12,
    paddingVertical: 8,
  },
  filterText: {
    fontSize: 13.6,
    color: '#FFFFFF',
    marginRight: 8,
  },
  orderCard: {
    backgroundColor: 'rgba(217, 217, 217, 0.05)',
    borderRadius: 8,
    padding: 16,
    marginBottom: 12,
    minHeight: 148,
  },
  orderHeader: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    marginBottom: 12,
  },
  orderNumber: {
    fontSize: 16,
    fontWeight: '500',
    color: '#FFFFFF',
  },
  statusBadge: {
    paddingHorizontal: 12,
    paddingVertical: 4,
    borderRadius: 20,
  },
  statusText: {
    fontSize: 12,
    fontWeight: '500',
    textAlign: 'center',
  },
  products: {
    fontSize: 16,
    color: 'rgba(255, 255, 255, 0.8)',
    marginBottom: 16,
  },
  orderFooter: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'flex-end',
  },
  priceSection: {
    flex: 1,
  },
  commissionSection: {
    flex: 1,
    marginLeft: 16,
  },
  dateSection: {
    alignItems: 'flex-end',
  },
  label: {
    fontSize: 14,
    color: 'rgba(255, 255, 255, 0.6)',
    marginBottom: 4,
  },
  price: {
    fontSize: 16,
    fontWeight: '500',
    color: '#FFFFFF',
  },
  commission: {
    fontSize: 16,
    fontWeight: '500',
    color: '#4ADE80',
  },
  date: {
    fontSize: 14,
    color: 'rgba(255, 255, 255, 0.6)',
  },
  buttonContainer: {
    paddingHorizontal: 16,
    paddingVertical: 8,
  },
  withdrawalButton: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    backgroundColor: '#2160F2',
    height: 48,
    borderRadius: 8,
    shadowColor: '#000',
    shadowOffset: {
      width: 0,
      height: 4,
    },
    shadowOpacity: 0.25,
    shadowRadius: 6,
    elevation: 8,
  },
  withdrawalText: {
    fontSize: 16,
    fontWeight: '700',
    color: '#FFFFFF',
    letterSpacing: 0.21,
    marginLeft: 10,
  },
  navigation: {
    height: 67,
    backgroundColor: '#182134',
    flexDirection: 'row',
    shadowColor: '#000',
    shadowOffset: {
      width: 0,
      height: -3,
    },
    shadowOpacity: 0.25,
    shadowRadius: 6,
    elevation: 8,
  },
  navItem: {
    flex: 1,
    alignItems: 'center',
    justifyContent: 'center',
    paddingVertical: 12,
  },
  navText: {
    fontSize: 12,
    fontWeight: '500',
    color: 'rgba(255, 255, 255, 0.3)',
    letterSpacing: 0.18,
    marginTop: 4,
  },
  navTextActive: {
    color: '#FFFFFF',
  },
});

export default AffiliateHomeScreen;