import React, { useState } from 'react';
import {
  View,
  Text,
  StyleSheet,
  ScrollView,
  TouchableOpacity,
  SafeAreaView,
  TextInput,
  FlatList,
} from 'react-native';
import { Ionicons } from '@expo/vector-icons';

const OrderQueueScreen = () => {
  const [searchText, setSearchText] = useState('');
  const [selectedFilters, setSelectedFilters] = useState(['Priority', 'Unattempted']);

  const filters = [
    { id: 'priority', label: 'Priority', selected: true },
    { id: 'unattempted', label: 'Unattempted', selected: true },
    { id: 'attempted', label: 'Attempted', selected: false },
    { id: 'completed', label: 'Completed', selected: false },
  ];

  const orders = [
    {
      id: 1,
      customer: 'Layla Hassan',
      orderId: '12345',
      items: 2,
      phone: '+966501234567',
    },
    {
      id: 2,
      customer: 'Omar Khalil',
      orderId: '67890',
      items: 1,
      phone: '+966501234568',
    },
    {
      id: 3,
      customer: 'Fatima Ali',
      orderId: '11223',
      items: 3,
      phone: '+966501234569',
    },
    {
      id: 4,
      customer: 'Ahmed Salem',
      orderId: '44556',
      items: 1,
      phone: '+966501234570',
    },
    {
      id: 5,
      customer: 'Fatima Ali',
      orderId: '11223',
      items: 3,
      phone: '+966501234569',
    },
    {
      id: 6,
      customer: 'Ahmed Salem',
      orderId: '44556',
      items: 1,
      phone: '+966501234570',
    },
  ];

  const handleFilterPress = (filterId) => {
    setSelectedFilters(prev => {
      if (prev.includes(filterId)) {
        return prev.filter(id => id !== filterId);
      } else {
        return [...prev, filterId];
      }
    });
  };

  const handleCallCustomer = (customer, phone) => {
    // Handle call functionality
    console.log(`Calling ${customer} at ${phone}`);
    // You would integrate with a calling service here
  };

  const FilterChip = ({ filter }) => {
    const isSelected = selectedFilters.includes(filter.id);
    return (
      <TouchableOpacity
        style={[styles.filterChip, isSelected && styles.filterChipSelected]}
        onPress={() => handleFilterPress(filter.id)}
      >
        <Text style={styles.filterChipText}>{filter.label}</Text>
        <Ionicons 
          name="chevron-down" 
          size={20} 
          color="#FFFFFF" 
          style={styles.filterChipIcon}
        />
      </TouchableOpacity>
    );
  };

  const OrderItem = ({ order }) => (
    <View style={styles.orderItem}>
      <View style={styles.orderInfo}>
        <Text style={styles.customerName}>Customer: {order.customer}</Text>
        <Text style={styles.orderDetails}>
          Order ID: {order.orderId} | {order.items} item{order.items > 1 ? 's' : ''}
        </Text>
      </View>
      <TouchableOpacity
        style={styles.callButton}
        onPress={() => handleCallCustomer(order.customer, order.phone)}
      >
        <Text style={styles.callButtonText}>Call Customer</Text>
      </TouchableOpacity>
    </View>
  );

  return (
    <SafeAreaView style={styles.container}>
      <View style={styles.content}>
        {/* Header */}
        <View style={styles.header}>
          <View style={styles.titleContainer}>
            <Text style={styles.title}>Order Queue</Text>
          </View>
          <TouchableOpacity style={styles.headerButton}>
            <Ionicons name="notifications-outline" size={24} color="#FFFFFF" />
          </TouchableOpacity>
        </View>

        {/* Search Bar */}
        <View style={styles.searchContainer}>
          <View style={styles.searchBar}>
            <View style={styles.searchIconContainer}>
              <Ionicons name="search" size={24} color="#94A6C7" />
            </View>
            <TextInput
              style={styles.searchInput}
              placeholder="Search orders"
              placeholderTextColor="#94A6C7"
              value={searchText}
              onChangeText={setSearchText}
            />
          </View>
        </View>

        {/* Filter Chips */}
        <View style={styles.filtersContainer}>
          <ScrollView
            horizontal
            showsHorizontalScrollIndicator={false}
            contentContainerStyle={styles.filtersScrollContent}
          >
            {filters.map((filter) => (
              <FilterChip key={filter.id} filter={filter} />
            ))}
          </ScrollView>
        </View>

        {/* Orders List */}
        <FlatList
          data={orders}
          keyExtractor={(item) => item.id.toString()}
          renderItem={({ item }) => <OrderItem order={item} />}
          showsVerticalScrollIndicator={false}
          style={styles.ordersList}
          contentContainerStyle={styles.ordersListContent}
        />
      </View>
    </SafeAreaView>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#121721',
  },
  content: {
    flex: 1,
    justifyContent: 'space-between',
  },
  header: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    paddingHorizontal: 16,
    paddingTop: 16,
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
  headerButton: {
    width: 48,
    height: 48,
    justifyContent: 'center',
    alignItems: 'center',
  },
  searchContainer: {
    paddingHorizontal: 16,
    paddingVertical: 12,
    height: 72,
  },
  searchBar: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#243047',
    borderRadius: 8,
    height: 48,
  },
  searchIconContainer: {
    width: 40,
    height: 48,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: '#243047',
    borderTopLeftRadius: 8,
    borderBottomLeftRadius: 8,
    paddingLeft: 16,
  },
  searchInput: {
    flex: 1,
    height: 48,
    paddingHorizontal: 8,
    paddingRight: 16,
    fontSize: 16,
    color: '#FFFFFF',
    backgroundColor: '#243047',
    borderTopRightRadius: 8,
    borderBottomRightRadius: 8,
  },
  filtersContainer: {
    paddingLeft: 12,
    paddingRight: 16,
    paddingVertical: 12,
    height: 56,
  },
  filtersScrollContent: {
    gap: 12,
    paddingHorizontal: 4,
  },
  filterChip: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    paddingLeft: 16,
    paddingRight: 8,
    height: 32,
    backgroundColor: '#243047',
    borderRadius: 8,
    gap: 8,
  },
  filterChipSelected: {
    backgroundColor: '#1F6BF5',
  },
  filterChipText: {
    fontFamily: 'Inter',
    fontWeight: '500',
    fontSize: 14,
    lineHeight: 21,
    textAlign: 'center',
    color: '#FFFFFF',
  },
  filterChipIcon: {
    width: 20,
    height: 20,
  },
  ordersList: {
    flex: 1,
  },
  ordersListContent: {
    paddingBottom: 20,
  },
  orderItem: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    paddingHorizontal: 16,
    paddingVertical: 8,
    height: 72,
    backgroundColor: '#121721',
    gap: 16,
  },
  orderInfo: {
    flex: 1,
    justifyContent: 'center',
  },
  customerName: {
    fontFamily: 'Inter',
    fontWeight: '500',
    fontSize: 16,
    lineHeight: 24,
    color: '#FFFFFF',
    marginBottom: 2,
  },
  orderDetails: {
    fontFamily: 'Inter',
    fontWeight: '400',
    fontSize: 14,
    lineHeight: 21,
    color: '#94A6C7',
  },
  callButton: {
    justifyContent: 'center',
    alignItems: 'center',
    paddingHorizontal: 16,
    height: 32,
    backgroundColor: '#243047',
    borderRadius: 8,
    minWidth: 84,
    maxWidth: 480,
  },
  callButtonText: {
    fontFamily: 'Inter',
    fontWeight: '500',
    fontSize: 14,
    lineHeight: 21,
    textAlign: 'center',
    color: '#FFFFFF',
  },
});

export default OrderQueueScreen;