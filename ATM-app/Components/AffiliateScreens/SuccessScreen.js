import React from 'react';
import {
  View,
  Text,
  TouchableOpacity,
  StyleSheet,
  SafeAreaView,
  Dimensions,
} from 'react-native';
import { Ionicons } from '@expo/vector-icons';

const { width } = Dimensions.get('window');

export default function SuccessScreen() {
  const handleDone = () => {
    console.log('Done pressed');
    // Handle done button press - navigate back or close modal
  };

  return (
    <SafeAreaView style={styles.container}>
      <View style={styles.content}>
        {/* Success Icon with Overlay */}
        <View style={styles.iconContainer}>
          <View style={styles.overlay} />
          <View style={styles.iconWrapper}>
            <Ionicons name="checkmark" size={60} color="#34D399" />
          </View>
        </View>

        {/* Success Title */}
        <Text style={styles.title}>Withdrawal Successful</Text>

        {/* Success Message */}
        <Text style={styles.message}>
          Your withdrawal request has been successfully processed. You will receive the funds in your account within 1-3 business days.
        </Text>

        {/* Done Button */}
        <TouchableOpacity style={styles.button} onPress={handleDone}>
          <Text style={styles.buttonText}>Done</Text>
        </TouchableOpacity>
      </View>
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#111722',
  },
  content: {
    flex: 1,
    alignItems: 'center',
    justifyContent: 'center',
    paddingHorizontal: 16,
  },
  iconContainer: {
    width: 219,
    height: 219,
    alignItems: 'center',
    justifyContent: 'center',
    marginBottom: 40,
    position: 'relative',
  },
  overlay: {
    position: 'absolute',
    width: 219,
    height: 219,
    backgroundColor: 'rgba(16, 185, 129, 0.1)',
    borderRadius: 219 / 2,
  },
  iconWrapper: {
    width: 80,
    height: 80,
    alignItems: 'center',
    justifyContent: 'center',
    zIndex: 1,
  },
  title: {
    width: 349,
    height: 35,
    fontFamily: 'Inter',
    fontWeight: '700',
    fontSize: 27.28,
    lineHeight: 34,
    textAlign: 'center',
    color: '#34D399',
    marginBottom: 20,
  },
  message: {
    width: 380,
    height: 71,
    fontFamily: 'Inter',
    fontWeight: '400',
    fontSize: 15.59,
    lineHeight: 23,
    textAlign: 'center',
    color: '#FFFFFF',
    marginBottom: 60,
    paddingHorizontal: 16,
  },
  button: {
    position: 'absolute',
    bottom: 60,
    left: 16,
    right: 15.18,
    height: 50.67,
    backgroundColor: '#2160F2',
    borderRadius: 7.79,
    justifyContent: 'center',
    alignItems: 'center',
    shadowColor: '#000',
    shadowOffset: {
      width: 0,
      height: 0.97,
    },
    shadowOpacity: 0.05,
    shadowRadius: 1.95,
    elevation: 2,
  },
  buttonText: {
    fontFamily: 'Inter',
    fontWeight: '700',
    fontSize: 15.59,
    lineHeight: 23,
    color: '#FFFFFF',
    textAlign: 'center',
  },
});