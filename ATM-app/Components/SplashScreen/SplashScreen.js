import React, { useEffect } from 'react';
import {
  View,
  Text,
  StyleSheet,
  Animated,
  StatusBar,
} from 'react-native';

const SplashScreen = ({ navigation }) => {
  const fadeAnim = new Animated.Value(0);
  const scaleAnim = new Animated.Value(0.8);

  useEffect(() => {
    // Start animations
    Animated.parallel([
      Animated.timing(fadeAnim, {
        toValue: 1,
        duration: 1200,
        useNativeDriver: true,
      }),
      Animated.spring(scaleAnim, {
        toValue: 1,
        tension: 50,
        friction: 7,
        useNativeDriver: true,
      }),
    ]).start();

    // Navigate to SignIn after 3 seconds
    const timer = setTimeout(() => {
      navigation.replace('signin');
    }, 3000);

    return () => clearTimeout(timer);
  }, [navigation]);

  return (
    <View style={styles.container}>
      <StatusBar barStyle="light-content" backgroundColor="#1a1f2e" />
      
      <Animated.View
        style={[
          styles.logoContainer,
          {
            opacity: fadeAnim,
            transform: [{ scale: scaleAnim }],
          },
        ]}
      >
        {/* BMG Logo */}
        <View style={styles.logoWrapper}>
          <Text style={styles.bmgText}>BMG</Text>
          <View style={styles.underline} />
          <View style={styles.corporationRow}>
            <Text style={styles.corporationText}>corporation</Text>
            <View style={styles.copyrightContainer}>
              <Text style={styles.copyrightSymbol}>©</Text>
            </View>
          </View>
        </View>
      </Animated.View>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    paddingBottom: 104,
    backgroundColor: '#1a1f2e', // Dark navy blue background
   
  },
  logoContainer: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    width: '100%',
  },
  logoWrapper: {
    alignItems: 'center',
    justifyContent: 'center',
  },
  bmgText: {
    fontSize: 72,
    fontWeight: '900',
    color: '#FFFFFF',
    letterSpacing: 8,
    marginBottom: 8,
    fontFamily: 'System', // Use system font or specify custom font
  },
  underline: {
    width: 280,
    height: 4,
    backgroundColor: '#FFFFFF',
    marginBottom: 12,
  },
  corporationRow: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
  },
  corporationText: {
    fontSize: 18,
    fontWeight: '300',
    color: '#9ca3af',
    letterSpacing: 3,
    marginRight: 8,
  },
  copyrightContainer: {
    width: 20,
    height: 20,
    borderRadius: 10,
    borderWidth: 1.5,
    borderColor: '#9ca3af',
    justifyContent: 'center',
    alignItems: 'center',
    marginLeft: 4,
  },
  copyrightSymbol: {
    fontSize: 12,
    fontWeight: 'bold',
    color: '#9ca3af',
    textAlign: 'center',
  },
});

export default SplashScreen;