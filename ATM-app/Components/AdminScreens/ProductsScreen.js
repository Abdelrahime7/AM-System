import React, { useState } from 'react';
import {
  View,
  Text,
  StyleSheet,
  TouchableOpacity,
  ScrollView,
  SafeAreaView,
  TextInput,
  Image,
} from 'react-native';
import { Ionicons } from '@expo/vector-icons';

const ProductsScreen = () => {
  const [searchQuery, setSearchQuery] = useState('');

  const products = [
    {
      id: '12345',
      name: 'Wireless Headphones',
      price: '$29.99',
      status: 'active',
      image: 'headphones',
    },
    {
      id: '67890',
      name: 'Smart Watch',
      price: '$49.99',
      status: 'active',
      image: 'watch',
    },
    {
      id: '11223',
      name: 'Portable Charger',
      price: '$19.99',
      status: 'draft',
      image: 'charger',
    },
    {
      id: '44556',
      name: 'Bluetooth Speaker',
      price: '$99.99',
      status: 'inactive',
      image: 'speaker',
    },
  ];

  const getStatusColor = (status) => {
    switch (status) {
      case 'active':
        return '#4ADE80';
      case 'draft':
        return '#FACC15';
      case 'inactive':
        return '#F87171';
      default:
        return '#90A2CB';
    }
  };

  const getStatusBgColor = (status) => {
    switch (status) {
      case 'active':
        return 'rgba(34, 197, 94, 0.1)';
      case 'draft':
        return 'rgba(234, 179, 8, 0.1)';
      case 'inactive':
        return 'rgba(239, 68, 68, 0.1)';
      default:
        return 'rgba(144, 162, 203, 0.1)';
    }
  };

  const handleEdit = (productId) => {
    console.log('Editing product:', productId);
  };

  const handleDelete = (productId) => {
    console.log('Deleting product:', productId);
  };

  return (
    <SafeAreaView style={styles.container}>
      {/* Header */}
      <View style={styles.header}>
        <Text style={styles.headerTitle}>Product Management</Text>
        
        {/* Search Bar */}
        <View style={styles.searchContainer}>
          <View style={styles.searchIcon}>
            <Ionicons name="search-outline" size={24} color="#90A2CB" />
          </View>
          <TextInput
            style={styles.searchInput}
            placeholder="Search products by name or ID"
            placeholderTextColor="#90A2CB"
            value={searchQuery}
            onChangeText={setSearchQuery}
          />
        </View>

        {/* Filter Buttons */}
        <View style={styles.filterContainer}>
          <TouchableOpacity style={styles.filterButton}>
            <Text style={styles.filterText}>Category</Text>
            <Ionicons name="chevron-down" size={20} color="#FFFFFF" />
          </TouchableOpacity>
          <TouchableOpacity style={styles.filterButton}>
            <Text style={styles.filterText}>Status</Text>
            <Ionicons name="chevron-down" size={20} color="#FFFFFF" />
          </TouchableOpacity>
          <TouchableOpacity style={styles.filterButton}>
            <Text style={styles.filterText}>Stock</Text>
            <Ionicons name="chevron-down" size={20} color="#FFFFFF" />
          </TouchableOpacity>
        </View>
      </View>

      {/* Product List */}
      <ScrollView style={styles.content} showsVerticalScrollIndicator={false}>
        {products.map((product) => (
          <View key={product.id} style={styles.productCard}>
            <View style={styles.productImage}>
              <View style={styles.placeholderImage}>
                <Ionicons name="image-outline" size={32} color="#90A2CB" />
              </View>
            </View>
            
            <View style={styles.productInfo}>
              <Text style={styles.productName}>{product.name}</Text>
              <Text style={styles.productDetails}>ID: {product.id} | {product.price}</Text>
              
              <View style={[styles.statusBadge, { backgroundColor: getStatusBgColor(product.status) }]}>
                <Text style={[styles.statusText, { color: getStatusColor(product.status) }]}>
                  {product.status.charAt(0).toUpperCase() + product.status.slice(1)}
                </Text>
              </View>
            </View>

            <View style={styles.productActions}>
              <TouchableOpacity
                style={styles.actionButton}
                onPress={() => handleEdit(product.id)}
              >
                <Ionicons name="create-outline" size={24} color="rgba(255, 255, 255, 0.8)" />
              </TouchableOpacity>
              <TouchableOpacity
                style={styles.actionButton}
                onPress={() => handleDelete(product.id)}
              >
                <Ionicons name="trash-outline" size={24} color="rgba(239, 68, 68, 0.8)" />
              </TouchableOpacity>
            </View>
          </View>
        ))}
      </ScrollView>

      {/* Bottom Navigation */}
      <View style={styles.bottomNav}>
        <TouchableOpacity style={styles.navItem}>
          <Ionicons name="home-outline" size={24} color="#FFFFFF66" />
          <Text style={styles.navText}>Home</Text>
        </TouchableOpacity>
        <TouchableOpacity style={styles.navItem}>
          <Ionicons name="people-outline" size={24} color="#FFFFFF66" />
          <Text style={styles.navText}>Users</Text>
        </TouchableOpacity>
        <TouchableOpacity style={styles.navItem}>
          <Ionicons name="cube" size={24} color="#FFFFFF" />
          <Text style={styles.navActiveText}>Products</Text>
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
    backgroundColor: '#101623',
  },
  header: {
    backgroundColor: 'rgba(16, 22, 35, 0.8)',
    paddingHorizontal: 16,
    paddingVertical: 16,
    paddingBottom: 0,
  },
  headerTitle: {
    fontSize: 20,
    fontWeight: '700',
    color: '#FFFFFF',
    textAlign: 'center',
    marginBottom: 20,
  },
  searchContainer: {
    backgroundColor: '#222E49',
    borderRadius: 25,
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 16,
    marginBottom: 16,
    height: 48,
  },
  searchIcon: {
    marginRight: 12,
  },
  searchInput: {
    flex: 1,
    fontSize: 16,
    color: '#FFFFFF',
    height: '100%',
  },
  filterContainer: {
    flexDirection: 'row',
    gap: 12,
    marginBottom: 16,
  },
  filterButton: {
    backgroundColor: '#222E49',
    borderRadius: 20,
    paddingHorizontal: 16,
    paddingVertical: 10,
    flexDirection: 'row',
    alignItems: 'center',
    gap: 8,
  },
  filterText: {
    fontSize: 14,
    fontWeight: '500',
    color: '#FFFFFF',
  },
  content: {
    flex: 1,
    paddingHorizontal: 16,
    paddingTop: 16,
  },
  productCard: {
    backgroundColor: '#182134',
    borderRadius: 8,
    padding: 12,
    flexDirection: 'row',
    alignItems: 'center',
    marginBottom: 16,
  },
  productImage: {
    marginRight: 16,
  },
  placeholderImage: {
    width: 64,
    height: 64,
    backgroundColor: '#243047',
    borderRadius: 8,
    justifyContent: 'center',
    alignItems: 'center',
  },
  productInfo: {
    flex: 1,
  },
  productName: {
    fontSize: 16,
    fontWeight: '600',
    color: '#FFFFFF',
    marginBottom: 4,
  },
  productDetails: {
    fontSize: 14,
    color: '#90A2CB',
    marginBottom: 8,
  },
  statusBadge: {
    alignSelf: 'flex-start',
    paddingHorizontal: 8,
    paddingVertical: 2,
    borderRadius: 10,
  },
  statusText: {
    fontSize: 12,
    fontWeight: '600',
  },
  productActions: {
    gap: 8,
  },
  actionButton: {
    width: 32,
    height: 32,
    justifyContent: 'center',
    alignItems: 'center',
  },
  bottomNav: {
    backgroundColor: '#192433',
    flexDirection: 'row',
    paddingVertical: 9,
    paddingHorizontal: 16,
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

export default ProductsScreen;
