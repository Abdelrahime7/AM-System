import React, { useState } from 'react';
import {
  View,
  Text,
  StyleSheet,
  ScrollView,
  TouchableOpacity,
  SafeAreaView,
  StatusBar,
  TextInput,
  Image,
  Dimensions,
} from 'react-native';
import { Ionicons } from '@expo/vector-icons';

const { width } = Dimensions.get('window');

const AffiliateProductsScreen = () => {
  const [searchText, setSearchText] = useState('');

  const products = [
    {
      id: 1,
      title: 'Premium Organic Coffee Beans',
      description: 'Freshly roasted, rich aroma',
      image: require('../../assets/tea.jpg'),
      isSponsored: true,
    },
    {
      id: 2,
      title: 'Artisan Chocolate Bar',
      description: 'Dark chocolate with sea salt',
      image: require('../../assets/tea.jpg'),
      isSponsored: false,
    },
    {
      id: 3,
      title: 'Gourmet Tea Selection',
      description: 'Assorted flavors, herbal blends',
      image: require('../../assets/tea.jpg'),
      isSponsored: false,
    },
    {
      id: 4,
      title: 'Handcrafted Olive Oil',
      description: 'Extra virgin, cold-pressed',
      image: require('../../assets/tea.jpg'),
      isSponsored: false,
    },
    {
      id: 5,
      title: 'Gourmet Tea Selection',
      description: 'Assorted flavors, herbal blends',
      image: require('../../assets/tea.jpg'),
      isSponsored: false,
    },
    {
      id: 6,
      title: 'Handcrafted Olive Oil',
      description: 'Extra virgin, cold-pressed',
      image: require('../../assets/tea.jpg'),
      isSponsored: false,
    },
  ];

  const ProductCard = ({ product }) => (
    <View style={styles.productCard}>
      <View style={styles.productContent}>
        {product.isSponsored && (
          <Text style={styles.sponsoredLabel}>Sponsored</Text>
        )}
        <Text style={styles.productTitle}>{product.title}</Text>
        <Text style={styles.productDescription}>{product.description}</Text>
      </View>
      <View style={styles.productImageContainer}>
<Image source={product.image} />
        <View style={styles.productImagePlaceholder} />
      </View>
    </View>
  );

  const FilterButton = ({ title, hasIcon = true }) => (
    <TouchableOpacity style={styles.filterButton}>
      <Text style={styles.filterButtonText}>{title}</Text>
      {hasIcon && (
        <Ionicons name="chevron-down" size={16} color="#FFFFFF" />
      )}
    </TouchableOpacity>
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
        <Text style={styles.headerTitle}>Products</Text>
        <TouchableOpacity style={styles.headerButton}>
          <Ionicons name="menu" size={24} color="#6B7280" />
        </TouchableOpacity>
      </View>

      {/* Search Bar */}
      <View style={styles.searchContainer}>
        <View style={styles.searchIconContainer}>
          <Ionicons name="search" size={20} color="#92A7C9" />
        </View>
        <TextInput
          style={styles.searchInput}
          placeholder="Search products"
          placeholderTextColor="#92A7C9"
          value={searchText}
          onChangeText={setSearchText}
        />
      </View>

      {/* Filter Buttons */}
      <View style={styles.filtersContainer}>
        <ScrollView 
          horizontal 
          showsHorizontalScrollIndicator={false}
          contentContainerStyle={styles.filtersContent}
        >
          <FilterButton title="Category" />
          <FilterButton title="Status" />
          <FilterButton title="Sort" />
        </ScrollView>
      </View>

      {/* Products List */}
      <ScrollView 
        style={styles.productsContainer}
        showsVerticalScrollIndicator={false}
        contentContainerStyle={styles.productsContent}
      >
        {products.map((product) => (
          <ProductCard key={product.id} product={product} />
        ))}
      </ScrollView>

      {/* Create Custom Product Button */}
      <View style={styles.buttonContainer}>
        <TouchableOpacity style={styles.createProductButton}>
          <Text style={styles.createProductText}>+ Create Custom Product</Text>
        </TouchableOpacity>
      </View>

      {/* Bottom Navigation */}
      <View style={styles.navigation}>
        <NavItem title="Home" iconName="home-outline" />
        <NavItem title="Products" iconName="cube-outline" active />
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
    height: 70,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    paddingHorizontal: 16,
    backgroundColor: '#111722',
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
  searchContainer: {
    flexDirection: 'row',
    marginHorizontal: 16,
    marginTop: 12,
    marginBottom: 16,
    backgroundColor: '#233148',
    borderRadius: 8,
    height: 48,
    alignItems: 'center',
  },
  searchIconContainer: {
    width: 40,
    height: '100%',
    backgroundColor: '#233148',
    borderTopLeftRadius: 8,
    borderBottomLeftRadius: 8,
    alignItems: 'center',
    justifyContent: 'center',
  },
  searchInput: {
    flex: 1,
    height: '100%',
    backgroundColor: '#233148',
    borderTopRightRadius: 8,
    borderBottomRightRadius: 8,
    paddingHorizontal: 8,
    fontSize: 15.7,
    color: '#FFFFFF',
    fontFamily: 'Inter',
  },
  filtersContainer: {
    height: 55,
    marginBottom: 16,
  },
  filtersContent: {
    paddingHorizontal: 16,
    alignItems: 'center',
  },
  filterButton: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#233148',
    paddingHorizontal: 16,
    paddingVertical: 8,
    borderRadius: 8,
    marginRight: 12,
    height: 32,
  },
  filterButtonText: {
    fontSize: 13.7,
    fontWeight: '500',
    color: '#FFFFFF',
    marginRight: 8,
  },
  productsContainer: {
    flex: 1,
  },
  productsContent: {
    paddingHorizontal: 16,
    paddingBottom: 20,
  },
  productCard: {
    flexDirection: 'row',
    marginBottom: 20,
    alignItems: 'flex-start',
  },
  productContent: {
    flex: 1,
    paddingRight: 16,
  },
  sponsoredLabel: {
    fontSize: 13.7,
    fontWeight: '400',
    color: '#92A7C9',
    marginBottom: 4,
  },
  productTitle: {
    fontSize: 15.7,
    fontWeight: '700',
    color: '#FFFFFF',
    lineHeight: 20,
    marginBottom: 8,
  },
  productDescription: {
    fontSize: 13.7,
    fontWeight: '400',
    color: '#92A7C9',
    lineHeight: 21,
  },
  productImageContainer: {
    width: 110,
    height: 89,
  },
  productImagePlaceholder: {
    width: '100%',
    height: '100%',
    backgroundColor: '#233148',
    borderRadius: 8,
  },
  buttonContainer: {
    paddingHorizontal: 16,
    paddingVertical: 12,
  },
  createProductButton: {
    backgroundColor: '#2160F2',
    height: 49,
    borderRadius: 8,
    alignItems: 'center',
    justifyContent: 'center',
    shadowColor: '#000',
    shadowOffset: {
      width: 0,
      height: 4,
    },
    shadowOpacity: 0.25,
    shadowRadius: 6,
    elevation: 8,
  },
  createProductText: {
    fontSize: 16,
    fontWeight: '700',
    color: '#FFFFFF',
    letterSpacing: 0.21,
  },
  navigation: {
    height: 68,
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
    fontSize: 12.3,
    fontWeight: '500',
    color: 'rgba(255, 255, 255, 0.3)',
    letterSpacing: 0.18,
    marginTop: 4,
  },
  navTextActive: {
    color: '#FFFFFF',
  },
});

export default AffiliateProductsScreen;