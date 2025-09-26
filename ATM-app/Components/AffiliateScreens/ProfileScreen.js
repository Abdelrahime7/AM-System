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
  Alert,
} from 'react-native';
import { Ionicons } from '@expo/vector-icons';

const ProfileScreen = () => {
  const [formData, setFormData] = useState({
    name: 'Alex Johnson',
    email: 'alex.johnson@example.com',
    phone: '+1 (555) 123-4567',
    ccpNumber: '123457890',
  });
  const [language, setLanguage] = useState('EN');

  const handleInputChange = (field, value) => {
    setFormData(prev => ({
      ...prev,
      [field]: value
    }));
  };

  const handleSaveChanges = () => {
    Alert.alert(
      'Success',
      'Your profile changes have been saved successfully.',
      [{ text: 'OK' }]
    );
  };

  const handleLogout = () => {
    Alert.alert(
      'Logout',
      'Are you sure you want to logout?',
      [
        { text: 'Cancel', style: 'cancel' },
        { 
          text: 'Logout', 
          style: 'destructive',
          onPress: () => {
            // Handle logout logic
            console.log('User logged out');
          }
        }
      ]
    );
  };

  const handleChangePassword = () => {
    // Navigate to change password screen
    console.log('Change password pressed');
  };

  const toggleLanguage = () => {
    setLanguage(prev => prev === 'EN' ? 'AR' : 'EN');
  };

  const handleEditProfilePicture = () => {
    Alert.alert(
      'Edit Profile Picture',
      'Choose an option',
      [
        { text: 'Camera', onPress: () => console.log('Camera selected') },
        { text: 'Gallery', onPress: () => console.log('Gallery selected') },
        { text: 'Cancel', style: 'cancel' }
      ]
    );
  };

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
      
      <ScrollView 
        style={styles.scrollView}
        showsVerticalScrollIndicator={false}
        contentContainerStyle={styles.scrollContent}
      >
        {/* Profile Header */}
        <View style={styles.profileHeader}>
          <View style={styles.profileImageContainer}>
            <View style={styles.profileImage}>
              {/* Profile picture placeholder - replace with actual Image component */}
              <View style={styles.profileImagePlaceholder} />
            </View>
            <TouchableOpacity 
              style={styles.editProfileButton}
              onPress={handleEditProfilePicture}
            >
              <Ionicons name="pencil" size={18} color="#FFFFFF" />
            </TouchableOpacity>
          </View>
          
          <Text style={styles.profileName}>{formData.name}</Text>
          <Text style={styles.affiliateId}>Affiliate ID: 123456</Text>
        </View>

        {/* Personal Information Section */}
        <View style={styles.section}>
          <Text style={styles.sectionTitle}>Personal Information</Text>
          
          {/* Name Field */}
          <View style={styles.inputGroup}>
            <Text style={styles.inputLabel}>Name</Text>
            <View style={styles.inputContainer}>
              <TextInput
                style={styles.input}
                value={formData.name}
                onChangeText={(value) => handleInputChange('name', value)}
                placeholderTextColor="#9CA3AF"
              />
            </View>
          </View>

          {/* Email Field */}
          <View style={styles.inputGroup}>
            <Text style={styles.inputLabel}>Email</Text>
            <View style={[styles.inputContainer, styles.disabledInput]}>
              <TextInput
                style={[styles.input, styles.disabledInputText]}
                value={formData.email}
                editable={false}
                placeholderTextColor="#9CA3AF"
              />
            </View>
          </View>

          {/* Phone Number Field */}
          <View style={styles.inputGroup}>
            <Text style={styles.inputLabel}>Phone Number</Text>
            <View style={styles.inputContainer}>
              <TextInput
                style={styles.input}
                value={formData.phone}
                onChangeText={(value) => handleInputChange('phone', value)}
                placeholderTextColor="#9CA3AF"
                keyboardType="phone-pad"
              />
            </View>
          </View>

          {/* CCP Number Field */}
          <View style={styles.inputGroup}>
            <Text style={styles.inputLabel}>CCP Number</Text>
            <View style={styles.inputContainer}>
              <TextInput
                style={styles.input}
                value={formData.ccpNumber}
                onChangeText={(value) => handleInputChange('ccpNumber', value)}
                placeholderTextColor="#9CA3AF"
                keyboardType="numeric"
              />
            </View>
          </View>
        </View>

        {/* Account Settings Section */}
        <View style={styles.section}>
          <Text style={styles.sectionTitle}>Account Settings</Text>
          
          <View style={styles.settingsContainer}>
            {/* Change Password */}
            <TouchableOpacity 
              style={styles.settingItem}
              onPress={handleChangePassword}
            >
              <Text style={styles.settingText}>Change Password</Text>
              <Ionicons name="chevron-forward" size={20} color="#9CA3AF" />
            </TouchableOpacity>

            {/* Language Setting */}
            <View style={[styles.settingItem, styles.settingItemBorder]}>
              <Text style={styles.settingText}>Language</Text>
              <View style={styles.languageToggle}>
                <TouchableOpacity 
                  style={[
                    styles.languageButton,
                    language === 'EN' && styles.languageButtonActive
                  ]}
                  onPress={() => setLanguage('EN')}
                >
                  <Text style={[
                    styles.languageButtonText,
                    language === 'EN' && styles.languageButtonTextActive
                  ]}>EN</Text>
                </TouchableOpacity>
                <TouchableOpacity 
                  style={[
                    styles.languageButton,
                    language === 'AR' && styles.languageButtonActive
                  ]}
                  onPress={() => setLanguage('AR')}
                >
                  <Text style={[
                    styles.languageButtonText,
                    language === 'AR' && styles.languageButtonTextActive
                  ]}>AR</Text>
                </TouchableOpacity>
              </View>
            </View>
          </View>
        </View>

        {/* Action Buttons */}
        <View style={styles.actionButtons}>
          <TouchableOpacity 
            style={styles.saveButton}
            onPress={handleSaveChanges}
          >
            <Text style={styles.saveButtonText}>Save Changes</Text>
          </TouchableOpacity>
          
          <TouchableOpacity 
            style={styles.logoutButton}
            onPress={handleLogout}
          >
            <Text style={styles.logoutButtonText}>Logout</Text>
          </TouchableOpacity>
        </View>
      </ScrollView>

      {/* Bottom Navigation */}
      <View style={styles.navigation}>
        <NavItem title="Home" iconName="home-outline" />
        <NavItem title="Products" iconName="cube-outline" />
        <NavItem title="Withdrawal" iconName="wallet-outline" />
        <NavItem title="Profile" iconName="person-outline" active />
      </View>
    </SafeAreaView>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#111722',
  },
  scrollView: {
    flex: 1,
  },
  scrollContent: {
    paddingBottom: 20,
  },
  profileHeader: {
    alignItems: 'center',
    paddingTop: 30,
    paddingBottom: 40,
  },
  profileImageContainer: {
    position: 'relative',
    marginBottom: 20,
  },
  profileImage: {
    width: 128,
    height: 128,
    borderRadius: 64,
    overflow: 'hidden',
  },
  profileImagePlaceholder: {
    width: '100%',
    height: '100%',
    backgroundColor: '#C8F1FF',
  },
  editProfileButton: {
    position: 'absolute',
    bottom: 0,
    right: 0,
    width: 32,
    height: 32,
    backgroundColor: '#2160F2',
    borderRadius: 16,
    alignItems: 'center',
    justifyContent: 'center',
  },
  profileName: {
    fontSize: 24,
    fontWeight: '700',
    color: '#FFFFFF',
    textAlign: 'center',
    lineHeight: 32,
    marginBottom: 6,
  },
  affiliateId: {
    fontSize: 14,
    fontWeight: '400',
    color: '#90A2CB',
    textAlign: 'center',
    lineHeight: 20,
  },
  section: {
    paddingHorizontal: 32,
    marginBottom: 32,
  },
  sectionTitle: {
    fontSize: 18,
    fontWeight: '600',
    color: '#FFFFFF',
    lineHeight: 28,
    marginBottom: 16,
  },
  inputGroup: {
    marginBottom: 16,
  },
  inputLabel: {
    fontSize: 14,
    fontWeight: '500',
    color: '#D1D5DB',
    lineHeight: 20,
    marginBottom: 8,
  },
  inputContainer: {
    backgroundColor: '#1A2333',
    borderWidth: 1,
    borderColor: '#2D3748',
    borderRadius: 8,
    height: 54,
    paddingHorizontal: 17,
    justifyContent: 'center',
  },
  disabledInput: {
    backgroundColor: 'rgba(34, 46, 73, 0.5)',
  },
  input: {
    fontSize: 16,
    color: '#FFFFFF',
    lineHeight: 19,
    padding: 0,
  },
  disabledInputText: {
    color: '#9CA3AF',
  },
  settingsContainer: {
    backgroundColor: '#1A2333',
    borderWidth: 1,
    borderColor: '#2D3748',
    borderRadius: 8,
    overflow: 'hidden',
  },
  settingItem: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 16,
    paddingVertical: 16,
    height: 56,
  },
  settingItemBorder: {
    borderTopWidth: 1,
    borderTopColor: '#2D3748',
  },
  settingText: {
    fontSize: 16,
    fontWeight: '400',
    color: '#FFFFFF',
    lineHeight: 24,
  },
  languageToggle: {
    flexDirection: 'row',
    backgroundColor: '#222E49',
    borderRadius: 18,
    height: 36,
    padding: 4,
  },
  languageButton: {
    paddingHorizontal: 16,
    paddingVertical: 8,
    borderRadius: 14,
    minWidth: 43,
    alignItems: 'center',
    justifyContent: 'center',
  },
  languageButtonActive: {
    backgroundColor: '#2160F2',
  },
  languageButtonText: {
    fontSize: 14,
    fontWeight: '400',
    color: '#9CA3AF',
    lineHeight: 20,
  },
  languageButtonTextActive: {
    fontWeight: '600',
    color: '#FFFFFF',
  },
  actionButtons: {
    paddingHorizontal: 33,
    gap: 16,
  },
  saveButton: {
    backgroundColor: '#2160F2',
    height: 48,
    borderRadius: 8,
    alignItems: 'center',
    justifyContent: 'center',
  },
  saveButtonText: {
    fontSize: 16,
    fontWeight: '700',
    color: '#FFFFFF',
    lineHeight: 24,
  },
  logoutButton: {
    backgroundColor: '#222E49',
    height: 48,
    borderRadius: 8,
    alignItems: 'center',
    justifyContent: 'center',
  },
  logoutButtonText: {
    fontSize: 16,
    fontWeight: '700',
    color: '#FFFFFF',
    lineHeight: 24,
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

export default ProfileScreen;