import React, { useState } from 'react';
import {
  View,
  Text,
  StyleSheet,
  ScrollView,
  TouchableOpacity,
  SafeAreaView,
  StatusBar,
} from 'react-native';
import { Ionicons } from '@expo/vector-icons';

const AdminAssistantDashboard = () => {
  const [orders, setOrders] = useState([
    { id: '12345', customer: 'Layla Hassan', amount: '$150.00' },
    { id: '12346', customer: 'Ahmed Ali', amount: '$150.00' },
    { id: '12347', customer: 'Sara Mohamed', amount: '$150.00' },
    { id: '12348', customer: 'Omar Ibrahim', amount: '$150.00' },
    { id: '12349', customer: 'Fatima Youssef', amount: '$150.00' },
  ]);

  const handleApprove = (orderId) => {
    console.log(`Approved order: ${orderId}`);
    // Add your approval logic here
  };

  const handleReject = (orderId) => {
    console.log(`Rejected order: ${orderId}`);
    // Add your rejection logic here
  };

  const OrderCard = ({ order }) => (
    <View style={styles.orderCard}>
      <View style={styles.orderInfo}>
        <Text style={styles.orderId}>Order ID: #{order.id}</Text>
        <Text style={styles.customerName}>Customer: {order.customer}</Text>
        <Text style={styles.amount}>{order.amount}</Text>
      </View>
      
      <View style={styles.actionButtons}>
        <TouchableOpacity
          style={styles.approveButton}
          onPress={() => handleApprove(order.id)}
        >
          <Text style={styles.approveText}>Approve</Text>
        </TouchableOpacity>
        
        <TouchableOpacity
          style={styles.rejectButton}
          onPress={() => handleReject(order.id)}
        >
          <Text style={styles.rejectText}>Reject</Text>
        </TouchableOpacity>
      </View>
    </View>
  );

  return (
    <SafeAreaView style={styles.container}>
      <StatusBar barStyle="light-content" backgroundColor="#121721" />
      
      {/* Header */}
      <View style={styles.header}>
        <View style={styles.titleContainer}>
          <Text style={styles.title}>Dashboard</Text>
        </View>
        
        <TouchableOpacity style={styles.menuButton}>
          <Ionicons name="menu" size={24} color="#FFFFFF" />
        </TouchableOpacity>
      </View>

      {/* Orders List */}
      <ScrollView
        style={styles.scrollView}
        showsVerticalScrollIndicator={false}
        contentContainerStyle={styles.scrollContent}
      >
        {orders.map((order, index) => (
          <OrderCard key={order.id} order={order} />
        ))}
      </ScrollView>
    </SafeAreaView>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#121721',
  },
  header: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    paddingHorizontal: 16,
    paddingVertical: 16,
    paddingBottom: 8,
    height: 72,
    backgroundColor: '#121721',
  },
  titleContainer: {
    flex: 1,
    alignItems: 'center',
    paddingLeft: 48,
  },
  title: {
    fontFamily: 'Inter',
    fontWeight: '700',
    fontSize: 18,
    lineHeight: 23,
    textAlign: 'center',
    color: '#FFFFFF',
  },
  menuButton: {
    width: 48,
    height: 48,
    justifyContent: 'flex-end',
    alignItems: 'center',
    paddingVertical: 12,
  },
  scrollView: {
    flex: 1,
  },
  scrollContent: {
    paddingHorizontal: 16,
    paddingBottom: 20,
  },
  orderCard: {
    width: '100%',
    height: 130,
    backgroundColor: '#182134',
    borderRadius: 8,
    marginBottom: 16,
    flexDirection: 'row',
    paddingHorizontal: 17,
    paddingVertical: 27,
    alignItems: 'flex-start',
    justifyContent: 'space-between',
  },
  orderInfo: {
    flex: 1,
    justifyContent: 'space-between',
    height: '100%',
  },
  orderId: {
    fontFamily: 'Inter',
    fontWeight: '500',
    fontSize: 16,
    lineHeight: 24,
    color: '#FFFFFF',
    marginBottom: 4,
  },
  customerName: {
    fontFamily: 'Inter',
    fontWeight: '400',
    fontSize: 14,
    lineHeight: 21,
    color: '#94A6C7',
    marginBottom: 8,
  },
  amount: {
    fontFamily: 'Inter',
    fontWeight: '400',
    fontSize: 16,
    lineHeight: 24,
    color: '#FFFFFF',
  },
  actionButtons: {
    justifyContent: 'space-between',
    height: 92,
    marginLeft: 10,
  },
  approveButton: {
    backgroundColor: 'rgba(34, 197, 94, 0.2)',
    borderRadius: 8,
    paddingHorizontal: 16,
    paddingVertical: 10,
    minWidth: 84,
    maxWidth: 480,
    height: 40,
    justifyContent: 'center',
    alignItems: 'center',
  },
  approveText: {
    fontFamily: 'Inter',
    fontWeight: '700',
    fontSize: 14,
    lineHeight: 21,
    textAlign: 'center',
    color: '#22C55E',
  },
  rejectButton: {
    backgroundColor: 'rgba(239, 68, 68, 0.2)',
    borderRadius: 8,
    paddingHorizontal: 16,
    paddingVertical: 10,
    minWidth: 84,
    maxWidth: 480,
    height: 40,
    justifyContent: 'center',
    alignItems: 'center',
  },
  rejectText: {
    fontFamily: 'Inter',
    fontWeight: '700',
    fontSize: 14,
    lineHeight: 21,
    textAlign: 'center',
    color: '#EF4444',
  },
});

export default AdminAssistantDashboard;