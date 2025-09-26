import React, { useState } from 'react';
import {
  View,
  Text,
  StyleSheet,
  ScrollView,
  TouchableOpacity,
  SafeAreaView,
  Image,
} from 'react-native';
import { Ionicons } from '@expo/vector-icons';

const UpdateDeliveryScreen = () => {
  const [selectedStatus, setSelectedStatus] = useState('out-for-delivery');

  const deliveryStatuses = [
    { id: 'out-for-delivery', label: 'Out for delivery', selected: true },
    { id: 'delivered', label: 'Delivered', selected: false },
    { id: 'rejected', label: 'Rejected', selected: false },
    { id: 'no-response', label: 'No response', selected: false },
  ];

  const orderItems = [
    { id: 1, name: 'Product A', quantity: 2, image: null },
    { id: 2, name: 'Product B', quantity: 1, image: null },
  ];

  const handleStatusSelect = (statusId) => {
    setSelectedStatus(statusId);
  };

  const handleConfirmDelivery = () => {
    // Handle confirm delivery action
    console.log('Delivery confirmed with status:', selectedStatus);
  };

  const StatusOption = ({ status }) => (
    <TouchableOpacity
      style={styles.statusOption}
      onPress={() => handleStatusSelect(status.id)}
    >
      <Text style={styles.statusText}>{status.label}</Text>
      <View style={[
        styles.radioButton,
        selectedStatus === status.id && styles.radioButtonSelected
      ]}>
        {selectedStatus === status.id && <View style={styles.radioButtonInner} />}
      </View>
    </TouchableOpacity>
  );

  const OrderItem = ({ item }) => (
    <View style={styles.orderItem}>
      <View style={styles.productImage} />
      <View style={styles.orderItemInfo}>
        <Text style={styles.productName}>{item.name}</Text>
        <Text style={styles.productQuantity}>Quantity: {item.quantity}</Text>
      </View>
    </View>
  );

  const InfoCard = ({ icon, title, subtitle }) => (
    <View style={styles.infoCard}>
      <View style={styles.iconContainer}>
        <Ionicons name={icon} size={24} color="#FFFFFF" />
      </View>
      <View style={styles.infoContent}>
        <Text style={styles.infoTitle}>{title}</Text>
        <Text style={styles.infoSubtitle}>{subtitle}</Text>
      </View>
    </View>
  );

  return (
    <SafeAreaView style={styles.container}>
      <ScrollView style={styles.scrollView} showsVerticalScrollIndicator={false}>
        {/* Header */}
        <View style={styles.header}>
          <TouchableOpacity style={styles.backButton}>
            <Ionicons name="arrow-back" size={24} color="#FFFFFF" />
          </TouchableOpacity>
          <View style={styles.titleContainer}>
            <Text style={styles.title}>Update Delivery</Text>
          </View>
        </View>

        {/* Customer Information Section */}
        <View style={styles.section}>
          <Text style={styles.sectionTitle}>Customer Information</Text>
          
          <InfoCard 
            icon="person-outline"
            title="Customer Name"
            subtitle="Omar Hassan"
          />
          
          <InfoCard 
            icon="location-outline"
            title="Delivery Address"
            subtitle="123 Al-Nahda Street, Riyadh"
          />
          
          <InfoCard 
            icon="call-outline"
            title="Contact Number"
            subtitle="+966 55 123 4567"
          />
        </View>

        {/* Order Items Section */}
        <View style={styles.section}>
          <Text style={styles.sectionTitle}>Order Items</Text>
          
          {orderItems.map((item) => (
            <OrderItem key={item.id} item={item} />
          ))}
        </View>

        {/* Delivery Status Section */}
        <View style={styles.section}>
          <Text style={styles.sectionTitle}>Delivery Status</Text>
          
          <View style={styles.statusContainer}>
            {deliveryStatuses.map((status) => (
              <StatusOption key={status.id} status={status} />
            ))}
          </View>
        </View>
      </ScrollView>

      {/* Bottom Button */}
      <View style={styles.bottomContainer}>
        <TouchableOpacity 
          style={styles.confirmButton}
          onPress={handleConfirmDelivery}
        >
          <Text style={styles.confirmButtonText}>Confirm Delivery</Text>
        </TouchableOpacity>
      </View>
    </SafeAreaView>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#0F1724',
  },
  scrollView: {
    flex: 1,
  },
  header: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    paddingHorizontal: 16,
    paddingTop: 16,
    paddingBottom: 8,
    height: 72,
    backgroundColor: '#0F1724',
  },
  backButton: {
    width: 48,
    height: 48,
    justifyContent: 'center',
    alignItems: 'center',
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
  section: {
    paddingHorizontal: 16,
    marginBottom: 20,
  },
  sectionTitle: {
    fontFamily: 'Inter',
    fontWeight: '700',
    fontSize: 22,
    lineHeight: 28,
    color: '#FFFFFF',
    marginBottom: 12,
    paddingTop: 20,
  },
  infoCard: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#182134',
    borderRadius: 8,
    paddingHorizontal: 16,
    paddingVertical: 12,
    marginBottom: 12,
    minHeight: 72,
  },
  iconContainer: {
    width: 48,
    height: 48,
    backgroundColor: '#243047',
    borderRadius: 8,
    justifyContent: 'center',
    alignItems: 'center',
    marginRight: 16,
  },
  infoContent: {
    flex: 1,
    justifyContent: 'center',
  },
  infoTitle: {
    fontFamily: 'Inter',
    fontWeight: '500',
    fontSize: 16,
    lineHeight: 24,
    color: '#FFFFFF',
    marginBottom: 2,
  },
  infoSubtitle: {
    fontFamily: 'Inter',
    fontWeight: '400',
    fontSize: 14,
    lineHeight: 21,
    color: '#94A6C7',
  },
  orderItem: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#182134',
    borderRadius: 8,
    paddingHorizontal: 16,
    paddingVertical: 8,
    marginBottom: 12,
    minHeight: 72,
  },
  productImage: {
    width: 56,
    height: 56,
    backgroundColor: '#243047',
    borderRadius: 8,
    marginRight: 16,
  },
  orderItemInfo: {
    flex: 1,
    justifyContent: 'center',
  },
  productName: {
    fontFamily: 'Inter',
    fontWeight: '500',
    fontSize: 16,
    lineHeight: 24,
    color: '#FFFFFF',
    marginBottom: 2,
  },
  productQuantity: {
    fontFamily: 'Inter',
    fontWeight: '400',
    fontSize: 14,
    lineHeight: 21,
    color: '#94A6C7',
  },
  statusContainer: {
    gap: 12,
  },
  statusOption: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 15,
    paddingVertical: 15,
    borderWidth: 1,
    borderColor: '#304569',
    borderRadius: 8,
    height: 53,
  },
  statusText: {
    fontFamily: 'Inter',
    fontWeight: '500',
    fontSize: 14,
    lineHeight: 21,
    color: '#FFFFFF',
    flex: 1,
  },
  radioButton: {
    width: 20,
    height: 20,
    borderRadius: 10,
    borderWidth: 2,
    borderColor: '#304569',
    justifyContent: 'center',
    alignItems: 'center',
  },
  radioButtonSelected: {
    borderColor: '#2170F2',
  },
  radioButtonInner: {
    width: 12,
    height: 12,
    borderRadius: 6,
    backgroundColor: '#2170F2',
  },
  bottomContainer: {
    paddingHorizontal: 16,
    paddingVertical: 12,
    backgroundColor: '#0F1724',
  },
  confirmButton: {
    backgroundColor: '#1F6BF5',
    borderRadius: 8,
    height: 48,
    justifyContent: 'center',
    alignItems: 'center',
    paddingHorizontal: 20,
  },
  confirmButtonText: {
    fontFamily: 'Inter',
    fontWeight: '700',
    fontSize: 16,
    lineHeight: 24,
    textAlign: 'center',
    color: '#FFFFFF',
  },
});

export default UpdateDeliveryScreen;