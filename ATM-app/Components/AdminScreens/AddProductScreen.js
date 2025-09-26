import React, { useState } from 'react';
import {
  View,
  Text,
  TextInput,
  TouchableOpacity,
  StyleSheet,
  ScrollView,
  SafeAreaView,
} from 'react-native';
import { Ionicons } from '@expo/vector-icons';

const AddProductScreen = () => {
  const [productName, setProductName] = useState('');
  const [description, setDescription] = useState('');
  const [price, setPrice] = useState('');
  const [commissionRate, setCommissionRate] = useState('');

  const handleCancel = () => {
    // Handle cancel action
    console.log('Cancel pressed');
  };

  const handleSave = () => {
    // Handle save action
    const productData = {
      name: productName,
      description,
      price,
      commissionRate,
    };
    console.log('Save pressed:', productData);
  };

  const handleImageUpload = () => {
    // Handle image upload
    console.log('Upload images pressed');
  };

  return (
    <SafeAreaView style={styles.container}>
      <ScrollView style={styles.scrollView} showsVerticalScrollIndicator={false}>
        {/* Header */}
        <View style={styles.header}>
          <TouchableOpacity style={styles.backButton}>
            <Ionicons name="arrow-back" size={24} color="#FFFFFF" />
          </TouchableOpacity>
          <View style={styles.headerTitleContainer}>
            <Text style={styles.headerTitle}>Add Product</Text>
          </View>
        </View>

        {/* Product Name Input */}
        <View style={styles.inputSection}>
          <View style={styles.inputContainer}>
            <TextInput
              style={styles.input}
              placeholder="Enter product name"
              placeholderTextColor="#94A6C7"
              value={productName}
              onChangeText={setProductName}
            />
          </View>
        </View>

        {/* Description Input */}
        <View style={styles.descriptionSection}>
          <View style={styles.descriptionContainer}>
            <TextInput
              style={styles.descriptionInput}
              placeholder="Enter product description"
              placeholderTextColor="#94A6C7"
              value={description}
              onChangeText={setDescription}
              multiline
              textAlignVertical="top"
            />
          </View>
        </View>

        {/* Price Input */}
        <View style={styles.inputSection}>
          <View style={styles.inputContainer}>
            <TextInput
              style={styles.input}
              placeholder="Enter price"
              placeholderTextColor="#94A6C7"
              value={price}
              onChangeText={setPrice}
              keyboardType="numeric"
            />
          </View>
        </View>

        {/* Commission Rate Input */}
        <View style={styles.inputSection}>
          <View style={styles.inputContainer}>
            <TextInput
              style={styles.input}
              placeholder="Enter commission rate"
              placeholderTextColor="#94A6C7"
              value={commissionRate}
              onChangeText={setCommissionRate}
              keyboardType="numeric"
            />
          </View>
        </View>

        {/* Image Upload Section */}
        <View style={styles.uploadSection}>
          <TouchableOpacity style={styles.uploadContainer} onPress={handleImageUpload}>
            <View style={styles.uploadContent}>
              <Text style={styles.uploadTitle}>Upload Images</Text>
              <Text style={styles.uploadSubtitle}>Tap to upload product images</Text>
            </View>
          </TouchableOpacity>
        </View>

        {/* Action Buttons */}
        <View style={styles.actionSection}>
          <View style={styles.buttonRow}>
            <TouchableOpacity style={styles.cancelButton} onPress={handleCancel}>
              <Text style={styles.cancelButtonText}>Cancel</Text>
            </TouchableOpacity>
            <TouchableOpacity style={styles.saveButton} onPress={handleSave}>
              <Text style={styles.saveButtonText}>Save</Text>
            </TouchableOpacity>
          </View>
        </View>
      </ScrollView>
    </SafeAreaView>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#121721',
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
    backgroundColor: '#121721',
  },
  backButton: {
    width: 48,
    height: 48,
    justifyContent: 'center',
    alignItems: 'center',
  },
  headerTitleContainer: {
    flex: 1,
    alignItems: 'center',
    paddingRight: 48, // Balance the back button
  },
  headerTitle: {
    fontFamily: 'Inter',
    fontWeight: '700',
    fontSize: 18,
    lineHeight: 23,
    color: '#FFFFFF',
    textAlign: 'center',
  },
  inputSection: {
    paddingHorizontal: 16,
    paddingVertical: 12,
    height: 80,
  },
  inputContainer: {
    flex: 1,
    minWidth: 160,
    height: 56,
  },
  input: {
    flex: 1,
    paddingHorizontal: 16,
    backgroundColor: '#243347',
    borderRadius: 8,
    color: '#FFFFFF',
    fontSize: 16,
    lineHeight: 24,
    fontFamily: 'Inter',
  },
  descriptionSection: {
    paddingHorizontal: 16,
    paddingVertical: 12,
    height: 200,
  },
  descriptionContainer: {
    flex: 1,
    minWidth: 160,
    height: 176,
  },
  descriptionInput: {
    flex: 1,
    padding: 16,
    backgroundColor: '#243347',
    borderRadius: 8,
    color: '#FFFFFF',
    fontSize: 16,
    lineHeight: 24,
    fontFamily: 'Inter',
    minHeight: 144,
  },
  uploadSection: {
    padding: 16,
    height: 200,
  },
  uploadContainer: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    paddingVertical: 56,
    paddingHorizontal: 24,
    borderWidth: 2,
    borderColor: '#334766',
    borderStyle: 'dashed',
    borderRadius: 8,
  },
  uploadContent: {
    alignItems: 'center',
    gap: 8,
  },
  uploadTitle: {
    fontFamily: 'Inter',
    fontWeight: '700',
    fontSize: 18,
    lineHeight: 23,
    color: '#FFFFFF',
    textAlign: 'center',
  },
  uploadSubtitle: {
    fontFamily: 'Inter',
    fontWeight: '400',
    fontSize: 14,
    lineHeight: 21,
    color: '#FFFFFF',
    textAlign: 'center',
  },
  actionSection: {
    height: 64,
  },
  buttonRow: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    paddingHorizontal: 16,
    paddingVertical: 12,
    gap: 12,
  },
  cancelButton: {
    justifyContent: 'center',
    alignItems: 'center',
    paddingHorizontal: 16,
    width: 84,
    height: 40,
    backgroundColor: '#243347',
    borderRadius: 8,
  },
  cancelButtonText: {
    fontFamily: 'Inter',
    fontWeight: '700',
    fontSize: 14,
    lineHeight: 21,
    color: '#FFFFFF',
    textAlign: 'center',
  },
  saveButton: {
    justifyContent: 'center',
    alignItems: 'center',
    paddingHorizontal: 16,
    width: 84,
    height: 40,
    backgroundColor: '#2B73E8',
    borderRadius: 8,
  },
  saveButtonText: {
    fontFamily: 'Inter',
    fontWeight: '700',
    fontSize: 14,
    lineHeight: 21,
    color: '#FFFFFF',
    textAlign: 'center',
  },
});

export default AddProductScreen;