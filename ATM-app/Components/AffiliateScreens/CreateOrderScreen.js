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
  Alert,
} from 'react-native';
import { Ionicons } from '@expo/vector-icons';

const CreateOrderScreen = () => {
  const [formData, setFormData] = useState({
    customerName: '',
    customerPhone: '',
    customerCity: '',
    shippingAddress: '',
    productSearch: '',
  });

  const [selectedProducts, setSelectedProducts] = useState([
    { id: 1, name: 'Product A', quantity: 2, price: 20.00 },
    { id: 2, name: 'Product B', quantity: 1, price: 15.00 }
  ]);

  const calculateTotal = () => {
    return selectedProducts.reduce((total, product) => {
      return total + (product.price * product.quantity);
    }, 0);
  };

  const calculateCommission = () => {
    const total = calculateTotal();
    return total * 0.15; // 15% commission rate
  };

  const handleInputChange = (field, value) => {
    setFormData(prev => ({
      ...prev,
      [field]: value
    }));
  };

  const handleBackPress = () => {
    Alert.alert(
      'Discard Changes',
      'Are you sure you want to go back? Your changes will be lost.',
      [
        { text: 'Cancel', style: 'cancel' },
        { text: 'Discard', style: 'destructive', onPress: () => console.log('Navigate back') }
      ]
    );
  };

  const removeProduct = (productId) => {
    setSelectedProducts(prev => prev.filter(product => product.id !== productId));
  };

  const handleSubmitOrder = () => {
    // Validate form
    if (!formData.customerName.trim()) {
      Alert.alert('Error', 'Please enter customer name');
      return;
    }
    if (!formData.customerPhone.trim()) {
      Alert.alert('Error', 'Please enter customer phone number');
      return;
    }
    if (!formData.customerCity.trim()) {
      Alert.alert('Error', 'Please select customer city');
      return;
    }
    if (!formData.shippingAddress.trim()) {
      Alert.alert('Error', 'Please enter shipping address');
      return;
    }
    if (selectedProducts.length === 0) {
      Alert.alert('Error', 'Please select at least one product');
      return;
    }

    Alert.alert(
      'Order Created',
      `Your order has been submitted successfully!\nTotal: $${calculateTotal().toFixed(2)}\nCommission: $${calculateCommission().toFixed(2)}`,
      [
        {
          text: 'OK',
          onPress: () => {
            // Reset form and navigate back
            setFormData({
              customerName: '',
              customerPhone: '',
              customerCity: '',
              shippingAddress: '',
              productSearch: '',
            });
            setSelectedProducts([]);
          }
        }
      ]
    );
  };

  const ProductCard = ({ product, onRemove }) => (
    <View style={styles.productCard}>
      <View style={styles.productInfo}>
        <Text style={styles.productName}>{product.name}</Text>
        <Text style={styles.productQuantity}>Quantity: {product.quantity}</Text>
      </View>
      <Text style={styles.productPrice}>${product.price.toFixed(2)}</Text>
      <TouchableOpacity 
        style={styles.removeButton}
        onPress={() => onRemove(product.id)}
      >
        <Ionicons name="trash-outline" size={20} color="#94A3B8" />
      </TouchableOpacity>
    </View>
  );

  return (
    <SafeAreaView style={styles.container}>
      <StatusBar barStyle="light-content" backgroundColor="#101623" />
      
      {/* Header */}
      <View style={styles.header}>
        <TouchableOpacity style={styles.backButton} onPress={handleBackPress}>
          <Ionicons name="arrow-back" size={24} color="#FFFFFF" />
        </TouchableOpacity>
        <Text style={styles.headerTitle}>Create New Order</Text>
      </View>

      <ScrollView 
        style={styles.scrollView}
        showsVerticalScrollIndicator={false}
        contentContainerStyle={styles.scrollContent}
      >
        {/* Customer Information */}
        <View style={styles.formSection}>
          <View style={styles.inputGroup}>
            <TextInput
              style={styles.input}
              placeholder="Enter customer's full name"
              placeholderTextColor="#64748B"
              value={formData.customerName}
              onChangeText={(value) => handleInputChange('customerName', value)}
            />
          </View>

          <View style={styles.inputGroup}>
            <TextInput
              style={styles.input}
              placeholder="Enter customer's phone number"
              placeholderTextColor="#64748B"
              value={formData.customerPhone}
              onChangeText={(value) => handleInputChange('customerPhone', value)}
              keyboardType="phone-pad"
            />
          </View>

          <TouchableOpacity style={styles.dropdownInput}>
            <Text style={[
              styles.dropdownText,
              !formData.customerCity && styles.placeholderText
            ]}>
              {formData.customerCity || 'Select Customer City'}
            </Text>
            <Ionicons name="chevron-down" size={16} color="#64748B" />
          </TouchableOpacity>

          <View style={styles.inputGroup}>
            <TextInput
              style={styles.input}
              placeholder="Enter shipping address"
              placeholderTextColor="#64748B"
              value={formData.shippingAddress}
              onChangeText={(value) => handleInputChange('shippingAddress', value)}
              multiline
            />
          </View>
        </View>

        {/* Product Selection */}
        <View style={styles.section}>
          <Text style={styles.sectionTitle}>Product Selection</Text>
          
          <View style={styles.inputGroup}>
            <TextInput
              style={styles.input}
              placeholder="Search for products"
              placeholderTextColor="#64748B"
              value={formData.productSearch}
              onChangeText={(value) => handleInputChange('productSearch', value)}
            />
          </View>

          {/* Selected Products */}
          {selectedProducts.map((product) => (
            <ProductCard 
              key={product.id} 
              product={product} 
              onRemove={removeProduct}
            />
          ))}
        </View>

        {/* Order Summary */}
        <View style={styles.section}>
          <Text style={styles.sectionTitle}>Order Summary</Text>
          
          <View style={styles.summaryCard}>
            <View style={styles.summaryRow}>
              <Text style={styles.summaryLabel}>Estimated Total</Text>
              <Text style={styles.summaryValue}>${calculateTotal().toFixed(2)}</Text>
            </View>
            
            <View style={styles.summaryRow}>
              <Text style={styles.summaryLabel}>Potential Commission</Text>
              <Text style={styles.commissionValue}>${calculateCommission().toFixed(2)}</Text>
            </View>
          </View>
        </View>
      </ScrollView>

      {/* Submit Button */}
      <View style={styles.submitContainer}>
        <TouchableOpacity 
          style={[
            styles.submitButton,
            selectedProducts.length === 0 && styles.submitButtonDisabled
          ]}
          onPress={handleSubmitOrder}
          disabled={selectedProducts.length === 0}
        >
          <Text style={styles.submitButtonText}>Submit Order</Text>
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
    height: 66,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    paddingHorizontal: 16,
    backgroundColor: '#101623',
  },
  backButton: {
    position: 'absolute',
    left: 16,
    width: 41,
    height: 41,
    backgroundColor: '#182134',
    borderRadius: 21,
    alignItems: 'center',
    justifyContent: 'center',
  },
  headerTitle: {
    fontSize: 20.7,
    fontWeight: '700',
    color: '#FFFFFF',
    letterSpacing: -0.31,
    textAlign: 'center',
    lineHeight: 26,
  },
  scrollView: {
    flex: 1,
  },
  scrollContent: {
    paddingBottom: 20,
  },
  formSection: {
    paddingHorizontal: 17,
    paddingTop: 45,
  },
  section: {
    paddingHorizontal: 17,
    marginTop: 24,
  },
  sectionTitle: {
    fontSize: 18.6,
    fontWeight: '700',
    color: '#FFFFFF',
    lineHeight: 23,
    letterSpacing: -0.28,
    marginBottom: 16,
  },
  inputGroup: {
    marginBottom: 16,
  },
  input: {
    backgroundColor: '#182134',
    borderWidth: 1,
    borderColor: '#222E49',
    borderRadius: 8,
    height: 50,
    paddingHorizontal: 13,
    fontSize: 16.5,
    color: '#FFFFFF',
    lineHeight: 20,
  },
  dropdownInput: {
    backgroundColor: '#182134',
    borderWidth: 1,
    borderColor: '#222E49',
    borderRadius: 8,
    height: 50,
    paddingHorizontal: 17,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    marginBottom: 16,
  },
  dropdownText: {
    fontSize: 16.5,
    color: '#FFFFFF',
    lineHeight: 20,
  },
  placeholderText: {
    color: '#64748B',
  },
  productCard: {
    backgroundColor: '#182134',
    borderRadius: 8,
    padding: 12,
    marginBottom: 12,
    flexDirection: 'row',
    alignItems: 'center',
    minHeight: 71,
  },
  productInfo: {
    flex: 1,
    paddingRight: 12,
  },
  productName: {
    fontSize: 16.5,
    fontWeight: '500',
    color: '#FFFFFF',
    lineHeight: 25,
    marginBottom: 4,
  },
  productQuantity: {
    fontSize: 14.5,
    fontWeight: '400',
    color: '#94A3B8',
    lineHeight: 22,
  },
  productPrice: {
    fontSize: 16.5,
    fontWeight: '500',
    color: '#FFFFFF',
    lineHeight: 25,
    marginRight: 12,
  },
  removeButton: {
    width: 32,
    height: 32,
    alignItems: 'center',
    justifyContent: 'center',
  },
  summaryCard: {
    backgroundColor: '#182134',
    borderRadius: 8,
    padding: 17,
  },
  summaryRow: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    marginBottom: 12,
  },
  summaryLabel: {
    fontSize: 16.5,
    fontWeight: '400',
    color: '#94A3B8',
    lineHeight: 25,
  },
  summaryValue: {
    fontSize: 16.5,
    fontWeight: '500',
    color: '#FFFFFF',
    lineHeight: 25,
  },
  commissionValue: {
    fontSize: 16.5,
    fontWeight: '500',
    color: '#4ADE80',
    lineHeight: 25,
  },
  submitContainer: {
    paddingHorizontal: 17,
    paddingVertical: 16,
  },
  submitButton: {
    backgroundColor: '#2160F2',
    height: 50,
    borderRadius: 8,
    alignItems: 'center',
    justifyContent: 'center',
  },
  submitButtonDisabled: {
    backgroundColor: '#233148',
    opacity: 0.6,
  },
  submitButtonText: {
    fontSize: 18.6,
    fontWeight: '700',
    color: '#FFFFFF',
    lineHeight: 28,
    letterSpacing: 0.28,
  },
});

export default CreateOrderScreen