import React, { useState } from 'react';
import {
  View,
  Text,
  TextInput,
  TouchableOpacity,
  ScrollView,
  StyleSheet,
  Switch,
  SafeAreaView,
} from 'react-native';
import { Ionicons } from '@expo/vector-icons';

export default function ProductCreationForm() {
  const [productName, setProductName] = useState('');
  const [description, setDescription] = useState('');
  const [sizeEnabled, setSizeEnabled] = useState(true);
  const [colorEnabled, setColorEnabled] = useState(true);
  const [materialEnabled, setMaterialEnabled] = useState(true);

  const handleCreateProduct = () => {
    console.log('Creating product...');
    // Handle product creation logic here
  };

  const handleSaveAsDraft = () => {
    console.log('Saving as draft...');
    // Handle save as draft logic here
  };

  const handleUploadImages = () => {
    console.log('Uploading images...');
    // Handle image upload logic here
  };

  return (
    <SafeAreaView style={styles.container}>
      <ScrollView style={styles.scrollView} showsVerticalScrollIndicator={true}>
        {/* Header */}
        <View style={styles.header}>
          <Ionicons name="arrow-back" size={24} color="#FFFFFF" style={styles.backIcon} />
          <Text style={styles.headerTitle}>Create Product</Text>
        </View>

        {/* Product Name Input */}
        <View style={styles.inputContainer}>
          <View style={styles.inputWrapper}>
            <Text style={styles.inputLabel}>Product Name</Text>
            <TextInput
              style={styles.textInput}
              value={productName}
              onChangeText={setProductName}
              placeholder=""
              placeholderTextColor="#92A7C9"
            />
          </View>
        </View>

        {/* Description Input */}
        <View style={styles.textareaContainer}>
          <Text style={styles.textareaLabel}>Description</Text>
          <TextInput
            style={styles.textarea}
            value={description}
            onChangeText={setDescription}
            placeholder=""
            placeholderTextColor="#92A7C9"
            multiline={true}
            textAlignVertical="top"
          />
        </View>

        {/* Customizable Attributes Section */}
        <Text style={styles.sectionTitle}>Customizable Attributes</Text>

        {/* Size Option */}
        <View style={styles.attributeRow}>
          <View style={styles.iconContainer}>
            <Ionicons name="resize" size={24} color="#FFFFFF" />
          </View>
          <View style={styles.attributeContent}>
            <Text style={styles.attributeTitle}>Size</Text>
            <Text style={styles.attributeSubtitle}>Add size options</Text>
          </View>
          <Switch
            value={sizeEnabled}
            onValueChange={setSizeEnabled}
            trackColor={{ false: '#233148', true: '#233148' }}
            thumbColor={sizeEnabled ? '#FFFFFF' : '#FFFFFF'}
            style={styles.switch}
          />
        </View>

        {/* Color Option */}
        <View style={styles.attributeRow}>
          <View style={styles.iconContainer}>
            <Ionicons name="color-palette" size={24} color="#FFFFFF" />
          </View>
          <View style={styles.attributeContent}>
            <Text style={styles.attributeTitle}>Color</Text>
            <Text style={styles.attributeSubtitle}>Add color options</Text>
          </View>
          <Switch
            value={colorEnabled}
            onValueChange={setColorEnabled}
            trackColor={{ false: '#233148', true: '#233148' }}
            thumbColor={colorEnabled ? '#FFFFFF' : '#FFFFFF'}
            style={styles.switch}
          />
        </View>

        {/* Material Option */}
        <View style={styles.attributeRow}>
          <View style={styles.iconContainer}>
            <Ionicons name="layers" size={24} color="#FFFFFF" />
          </View>
          <View style={styles.attributeContent}>
            <Text style={styles.attributeTitle}>Material</Text>
            <Text style={styles.attributeSubtitle}>Add material options</Text>
          </View>
          <Switch
            value={materialEnabled}
            onValueChange={setMaterialEnabled}
            trackColor={{ false: '#233148', true: '#233148' }}
            thumbColor={materialEnabled ? '#FFFFFF' : '#FFFFFF'}
            style={styles.switch}
          />
        </View>

        {/* Upload Images Button */}
        <TouchableOpacity style={styles.uploadButton} onPress={handleUploadImages}>
          <Text style={styles.uploadButtonText}>Upload Images</Text>
        </TouchableOpacity>

        {/* Bottom Section */}
        <View style={styles.bottomSection}>
          <Text style={styles.bottomTitle}>Pricing</Text>
          <Text style={styles.bottomPrice}>Free</Text>
        </View>

        {/* Action Buttons */}
        <View style={styles.buttonContainer}>
          <TouchableOpacity style={styles.primaryButton} onPress={handleCreateProduct}>
            <Text style={styles.primaryButtonText}>Create Product</Text>
          </TouchableOpacity>
          <TouchableOpacity style={styles.secondaryButton} onPress={handleSaveAsDraft}>
            <Text style={styles.secondaryButtonText}>Save as Draft</Text>
          </TouchableOpacity>
        </View>
      </ScrollView>
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#111722',
  },
  scrollView: {
    flex: 1,
    width: 380,
        marginLeft:15

  },
  header: {
    height: 72,
    backgroundColor: '#111722',
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    position: 'relative',
  },
  backIcon: {
    position: 'absolute',
    left: 16,
  },
  headerTitle: {
    fontFamily: 'Inter',
    fontWeight: '700',
    fontSize: 20,
    lineHeight: 22,
    color: '#FFFFFF',
    letterSpacing: -0.27,
  },
  inputContainer: {
    marginHorizontal: 16,
    marginTop: 12,
    height: 52,
    backgroundColor: '#233148',
    borderRadius: 8,
  },
  inputWrapper: {
    flex: 1,
    paddingHorizontal: 16,
    paddingVertical: 16,
  },
  inputLabel: {
    fontFamily: 'Inter',
    fontWeight: '400',
    fontSize: 16,
    lineHeight: 19,
    color: '#92A7C9',
    marginBottom: 4,
  },
  textInput: {
    flex: 1,
    color: '#FFFFFF',
    fontSize: 16,
    fontFamily: 'Inter',
  },
  textareaContainer: {
    marginHorizontal: 16,
    marginTop: 24,
    height: 144,
    backgroundColor: '#233148',
    borderRadius: 8,
    padding: 16,
  },
  textareaLabel: {
    fontFamily: 'Inter',
    fontWeight: '400',
    fontSize: 16,
    lineHeight: 24,
    color: '#92A7C9',
    marginBottom: 8,
  },
  textarea: {
    flex: 1,
    color: '#FFFFFF',
    fontSize: 16,
    fontFamily: 'Inter',
  },
  sectionTitle: {
    marginLeft: 16,
    marginTop: 24,
    fontFamily: 'Inter',
    fontWeight: '700',
    fontSize: 22,
    lineHeight: 28,
    color: '#FFFFFF',
    letterSpacing: -0.33,
  },
  attributeRow: {
    height: 72,
    backgroundColor: '#111722',
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 16,
    marginTop: 12,
  },
  iconContainer: {
    width: 48,
    height: 48,
    backgroundColor: '#233148',
    borderRadius: 8,
    justifyContent: 'center',
    alignItems: 'center',
  },
  attributeContent: {
    flex: 1,
    marginLeft: 16,
  },
  attributeTitle: {
    fontFamily: 'Inter',
    fontWeight: '500',
    fontSize: 16,
    lineHeight: 24,
    color: '#FFFFFF',
  },
  attributeSubtitle: {
    fontFamily: 'Inter',
    fontWeight: '400',
    fontSize: 14,
    lineHeight: 21,
    color: '#92A7C9',
    marginTop: 2,
  },
  switch: {
    marginLeft: 16,
  },
  uploadButton: {
    width: 157,
    height: 40,
    backgroundColor: '#233148',
    borderRadius: 8,
    marginLeft: 16,
    marginTop: 24,
    justifyContent: 'center',
    alignItems: 'center',
  },
  uploadButtonText: {
    fontFamily: 'Inter',
    fontWeight: '700',
    fontSize: 14,
    lineHeight: 21,
    color: '#FFFFFF',
    letterSpacing: 0.21,
  },
  bottomSection: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    marginHorizontal: 16,
    marginTop: 40,
    marginBottom: 24,
  },
  bottomTitle: {
    fontFamily: 'Inter',
    fontWeight: '700',
    fontSize: 22,
    lineHeight: 28,
    color: '#FFFFFF',
    letterSpacing: -0.33,
  },
  bottomPrice: {
    fontFamily: 'Inter',
    fontWeight: '700',
    fontSize: 22,
    lineHeight: 28,
    color: '#FFFFFF',
    letterSpacing: -0.33,
  },
  buttonContainer: {
    flexDirection: 'row',
    marginHorizontal: 16,
    marginBottom: 32,
    gap: 16,
  },
  primaryButton: {
    flex: 1,
    height: 48,
    backgroundColor: '#2160F2',
    borderRadius: 8,
    justifyContent: 'center',
    alignItems: 'center',
  },
  primaryButtonText: {
    fontFamily: 'Inter',
    fontWeight: '700',
    fontSize: 14,
    lineHeight: 21,
    color: '#FFFFFF',
    letterSpacing: 0.21,
  },
  secondaryButton: {
    width: 120,
    height: 48,
    backgroundColor: '#233148',
    borderRadius: 8,
    justifyContent: 'center',
    alignItems: 'center',
  },
  secondaryButtonText: {
    fontFamily: 'Inter',
    fontWeight: '700',
    fontSize: 14,
    lineHeight: 21,
    color: '#FFFFFF',
    letterSpacing: 0.21,
  },
});