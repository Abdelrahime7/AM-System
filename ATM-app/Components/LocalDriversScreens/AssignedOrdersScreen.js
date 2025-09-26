import React from 'react';
import { View, Text, StyleSheet, ScrollView, Image, TouchableOpacity } from 'react-native';
import { Ionicons } from '@expo/vector-icons';

 const AssignedOrdersScreen = () => {
  const orders = [
    { id: 1, customerId: 125, orderNumber: 789012, customerName: 'Layla Ali' },
    { id: 2, customerId: 125, orderNumber: 789012, customerName: 'Layla Ali' },
    { id: 3, customerId: 125, orderNumber: 789012, customerName: 'Layla Ali' },
    { id: 4, customerId: 125, orderNumber: 789012, customerName: 'Layla Ali' },
    { id: 5, customerId: 125, orderNumber: 789012, customerName: 'Layla Ali' },
    { id: 6, customerId: 125, orderNumber: 789012, customerName: 'Layla Ali' },
  ];

  const OrderCard = ({ order }) => (
    <TouchableOpacity style={styles.orderCard}>
      <View style={styles.orderImage} />
      <View style={styles.orderInfo}>
        <Text style={styles.customerName}>Customer: {order.customerName}</Text>
        <Text style={styles.orderDetails}>ID: {order.customerId} | Order: {order.orderNumber}</Text>
      </View>
      <Ionicons name="chevron-back" size={24} color="#9CA3AF" style={styles.chevronIcon} />
    </TouchableOpacity>
  );

  return (
    <View style={styles.container}>
      {/* Header */}
      <View style={styles.header}>
        <View style={styles.headerContent}>
          <View style={styles.backButton} />
          <View style={styles.titleContainer}>
            <Text style={styles.title}>Assigned Orders</Text>
          </View>
        </View>
      </View>

      {/* Orders List */}
      <ScrollView style={styles.scrollView} showsVerticalScrollIndicator={false}>
        {orders.map((order) => (
          <OrderCard key={order.id} order={order} />
        ))}
      </ScrollView>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#0F1724',
    paddingTop:45
  },
  header: {
    width: '100%',
    height: 72,
    backgroundColor: '#0F1724',
  },
  headerContent: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    paddingHorizontal: 16,
    paddingTop: 16,
    paddingBottom: 8,
    height: 72,
  },
  backButton: {
    width: 48,
    height: 48,
  },
  titleContainer: {
    flex: 1,
    alignItems: 'center',
    paddingRight: 48,
  },
  title: {
    fontFamily: 'Inter',
    fontWeight: '700',
    fontSize: 18,
    lineHeight: 23,
    textAlign: 'center',
    color: '#FFFFFF',
  },
  scrollView: {
    flex: 1,
    paddingHorizontal: 16,
  },
  orderCard: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#182134',
    borderRadius: 8,
    padding: 12,
    marginBottom: 12,
    height: 92,
  },
  orderImage: {
    width: 64,
    height: 64,
    backgroundColor: '#2A3441',
    borderRadius: 8,
    marginRight: 17,
  },
  orderInfo: {
    flex: 1,
    justifyContent: 'center',
  },
  customerName: {
    fontFamily: 'Inter',
    fontWeight: '600',
    fontSize: 16,
    lineHeight: 24,
    color: '#FFFFFF',
    marginBottom: 4,
  },
  orderDetails: {
    fontFamily: 'Inter',
    fontWeight: '400',
    fontSize: 14,
    lineHeight: 20,
    color: '#90A2CB',
  },
  chevronIcon: {
    transform: [{ rotate: '180deg' }],
    marginLeft: 12,
  },
});

export default AssignedOrdersScreen