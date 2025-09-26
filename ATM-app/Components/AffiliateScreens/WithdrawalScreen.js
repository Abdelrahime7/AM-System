import React, { useState } from 'react';
import {
  View,
  Text,
  StyleSheet,
  TouchableOpacity,
  SafeAreaView,
  StatusBar,
  TextInput,
  Alert,
} from 'react-native';
import { Ionicons } from '@expo/vector-icons';

const WithdrawalScreen = () => {
  const [withdrawalAmount, setWithdrawalAmount] = useState('');
  const availableBalance = 1250.00;

  const handleBackPress = () => {
    // Handle back navigation
    console.log('Back pressed');
  };

  const handleSubmitRequest = () => {
    const amount = parseFloat(withdrawalAmount);
    
    if (!withdrawalAmount || amount <= 0) {
      Alert.alert('Invalid Amount', 'Please enter a valid withdrawal amount.');
      return;
    }
    
    if (amount > availableBalance) {
      Alert.alert('Insufficient Balance', 'Withdrawal amount exceeds available balance.');
      return;
    }
    
    Alert.alert(
      'Withdrawal Request',
      `Your withdrawal request for $${amount.toFixed(2)} has been submitted successfully.`,
      [
        {
          text: 'OK',
          onPress: () => {
            setWithdrawalAmount('');
            // Navigate back or to success screen
          }
        }
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
      
      {/* Header */}
      <View style={styles.header}>
        <TouchableOpacity style={styles.backButton} onPress={handleBackPress}>
          <Ionicons name="arrow-back" size={24} color="#FFFFFF" />
        </TouchableOpacity>
        <Text style={styles.headerTitle}>Withdrawal Request</Text>
      </View>

      {/* Main Content */}
      <View style={styles.content}>
        {/* Available Balance Section */}
        <View style={styles.balanceSection}>
          <Text style={styles.balanceLabel}>Available Balance</Text>
          <Text style={styles.balanceAmount}>${availableBalance.toLocaleString('en-US', { minimumFractionDigits: 2 })}</Text>
        </View>

        {/* Withdrawal Form */}
        <View style={styles.formSection}>
          <Text style={styles.inputLabel}>Amount to Withdraw</Text>
          <View style={styles.inputContainer}>
            <Text style={styles.currencySymbol}>$</Text>
            <TextInput
              style={styles.input}
              placeholder="0.00"
              placeholderTextColor="#92A7C9"
              value={withdrawalAmount}
              onChangeText={setWithdrawalAmount}
              keyboardType="numeric"
              returnKeyType="done"
            />
          </View>
        </View>

        {/* Submit Button */}
        <TouchableOpacity 
          style={[
            styles.submitButton, 
            (!withdrawalAmount || parseFloat(withdrawalAmount) <= 0) && styles.submitButtonDisabled
          ]}
          onPress={handleSubmitRequest}
          disabled={!withdrawalAmount || parseFloat(withdrawalAmount) <= 0}
        >
          <Text style={styles.submitButtonText}>Submit Request</Text>
        </TouchableOpacity>
      </View>

      {/* Bottom Navigation */}
      <View style={styles.navigation}>
        <NavItem title="Home" iconName="home-outline" />
        <NavItem title="Products" iconName="cube-outline" />
        <NavItem title="Withdrawal" iconName="wallet-outline" active />
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
    height: 72,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    paddingHorizontal: 16,
    backgroundColor: '#111722',
  },
  backButton: {
    position: 'absolute',
    left: 16,
    width: 24,
    height: 24,
    alignItems: 'center',
    justifyContent: 'center',
  },
  headerTitle: {
    fontSize: 20,
    fontWeight: '700',
    color: '#FFFFFF',
    letterSpacing: -0.27,
    textAlign: 'center',
  },
  content: {
    flex: 1,
    paddingHorizontal: 16,
    paddingTop: 37,
  },
  balanceSection: {
    alignItems: 'center',
    marginBottom: 83,
  },
  balanceLabel: {
    fontSize: 13.6,
    fontWeight: '400',
    color: '#9CA3AF',
    textAlign: 'center',
    marginBottom: 49,
  },
  balanceAmount: {
    fontSize: 35,
    fontWeight: '700',
    color: '#F9FAFB',
    textAlign: 'center',
    lineHeight: 39,
  },
  formSection: {
    marginBottom: 30,
  },
  inputLabel: {
    fontSize: 15.6,
    fontWeight: '500',
    color: '#F9FAFB',
    marginBottom: 16,
  },
  inputContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: '#233148',
    borderRadius: 12,
    height: 49,
    paddingHorizontal: 16,
  },
  currencySymbol: {
    fontSize: 15.6,
    fontWeight: '400',
    color: '#92A7C9',
    marginRight: 8,
  },
  input: {
    flex: 1,
    fontSize: 15.6,
    fontWeight: '400',
    color: '#FFFFFF',
    paddingVertical: 0,
  },
  submitButton: {
    backgroundColor: '#2160F2',
    height: 51,
    borderRadius: 8,
    alignItems: 'center',
    justifyContent: 'center',
    shadowColor: '#000',
    shadowOffset: {
      width: 0,
      height: 1,
    },
    shadowOpacity: 0.05,
    shadowRadius: 2,
    elevation: 2,
  },
  submitButtonDisabled: {
    backgroundColor: '#233148',
    opacity: 0.6,
  },
  submitButtonText: {
    fontSize: 15.6,
    fontWeight: '700',
    color: '#FFFFFF',
    textAlign: 'center',
    lineHeight: 23,
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

export default WithdrawalScreen;