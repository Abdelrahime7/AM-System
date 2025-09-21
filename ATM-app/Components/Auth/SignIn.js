import { useNavigation } from '@react-navigation/native';
import React, { useState } from 'react';
import {
  View,
  Text,
  TextInput,
  TouchableOpacity,
  StyleSheet,
  SafeAreaView,
  ScrollView,
  StatusBar,
} from 'react-native';

const SignIn = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [rememberMe, setRememberMe] = useState(false);

const navigation = useNavigation();


  const handleSignIn = () => {
    console.log('Sign in pressed');
    console.log('Email:', email);
    console.log('Password:', password);
    console.log('Remember Me:', rememberMe);
    // Add your sign in logic here
  };

  const handleForgotPassword = () => {
    console.log('Forgot password pressed');
    // Add your forgot password logic here
  };

  const handleCreateAccount = () => {
    console.log('Create account pressed');

    navigation.navigate("signup")

  };

  const toggleLanguage = () => {
    console.log('Language toggle pressed');
    // Add your language toggle logic here
  };

  const toggleRememberMe = () => {
    setRememberMe(!rememberMe);
  };

  return (
    <>
      <StatusBar barStyle="light-content" backgroundColor="#111722" />
      <SafeAreaView style={styles.container}>
        <ScrollView 
          contentContainerStyle={styles.scrollContainer} 
          showsVerticalScrollIndicator={false}
        >
          {/* Header */}
          <View style={styles.headerMargin}>
            <View style={styles.header}>
              <View style={styles.logoContainer}>
                {/* Add your logo component here */}
              </View>
              <TouchableOpacity 
                style={styles.languageButton} 
                onPress={toggleLanguage}
                activeOpacity={0.8}
              >
                <Text style={styles.languageText}>EN / AR</Text>
              </TouchableOpacity>
            </View>
          </View>

          {/* Main Content */}
          <View style={styles.main}>
            {/* Welcome Section */}
            <View style={styles.welcomeMargin}>
              <View style={styles.welcomeContainer}>
                <View style={styles.headingContainer}>
                  <Text style={styles.welcomeTitle}>Welcome Back</Text>
                </View>
                <View style={styles.subtitleContainer}>
                  <Text style={styles.subtitle}>Sign in to continue</Text>
                </View>
              </View>
            </View>

            {/* Form Section */}
            <View style={styles.formMargin}>
              <View style={styles.form}>
                {/* Email Input */}
                <View style={styles.emailInputContainer}>
                  <TextInput
                    style={styles.textInput}
                    placeholder="Email/Username"
                    placeholderTextColor="#9CA3AF"
                    value={email}
                    onChangeText={setEmail}
                    keyboardType="email-address"
                    autoCapitalize="none"
                    autoCorrect={false}
                  />
                </View>

                {/* Password Input */}
                <View style={styles.passwordInputContainer}>
                  <TextInput
                    style={styles.textInput}
                    placeholder="Password"
                    placeholderTextColor="#9CA3AF"
                    value={password}
                    onChangeText={setPassword}
                    secureTextEntry={true}
                    autoCapitalize="none"
                    autoCorrect={false}
                  />
                </View>

                {/* Remember Me & Forgot Password */}
                <View style={styles.optionsContainer}>
                  <TouchableOpacity
                    style={styles.rememberMeContainer}
                    onPress={toggleRememberMe}
                    activeOpacity={0.8}
                  >
                    <View style={[
                      styles.checkbox, 
                      rememberMe && styles.checkboxChecked
                    ]}>
                      {rememberMe && <Text style={styles.checkmark}>✓</Text>}
                    </View>
                    <Text style={styles.rememberMeLabel}>Remember Me</Text>
                  </TouchableOpacity>

                  <TouchableOpacity 
                    style={styles.forgotPasswordContainer}
                    onPress={handleForgotPassword}
                    activeOpacity={0.8}
                  >
                    <Text style={styles.forgotPasswordText}>Forgot Password?</Text>
                  </TouchableOpacity>
                </View>

                {/* Sign In Button */}
                <TouchableOpacity 
                  style={styles.signInButton} 
                  onPress={handleSignIn}
                  activeOpacity={0.9}
                >
                  <Text style={styles.signInButtonText}>Sign In</Text>
                </TouchableOpacity>
              </View>
            </View>

            {/* Separator Section */}
            <View style={styles.separatorMargin}>
              <View style={styles.separatorContainer}>
                <View style={styles.separatorLine} />
                <View style={styles.separatorTextContainer}>
                  <Text style={styles.separatorText}>or</Text>
                </View>
                <View style={styles.separatorLine} />
              </View>

              {/* Create Account Link */}
              <View style={styles.createAccountContainer}>
                <Text style={styles.newUserText}>New user? </Text>
                <TouchableOpacity 
                  onPress={handleCreateAccount}
                  activeOpacity={0.8}
                >
                  <Text style={styles.createAccountText}>Create Account</Text>
                </TouchableOpacity>
              </View>
            </View>

            {/* Footer */}
            <View style={styles.footerMargin}>
              <View style={styles.footer}>
                <Text style={styles.footerText}>© 2024 AffiliateApp. All rights reserved.</Text>
              </View>
            </View>
          </View>
        </ScrollView>
      </SafeAreaView>
    </>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#111722',
  },
  scrollContainer: {
    flexGrow: 1,
    paddingBottom: 104,
  },
  headerMargin: {
    paddingBottom: 40,
  },
  header: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    paddingHorizontal: 24,
    paddingTop: 24,
    height: 38,
  },
  logoContainer: {
    width: 107,
    height: 28,
    // Add your logo styling here
  },
  languageButton: {
    paddingVertical: 9,
    paddingHorizontal: 17,
    borderWidth: 1,
    borderColor: '#475569',
    borderRadius: 9999,
    width: 83,
    height: 38,
    justifyContent: 'center',
    alignItems: 'center',
  },
  languageText: {
    fontFamily: 'System',
    fontWeight: '500',
    fontSize: 14,
    lineHeight: 20,
    color: '#FFFFFF',
    textAlign: 'center',
  },
  main: {
    paddingHorizontal: 24,
    paddingVertical: 54,
    justifyContent: 'center',
    alignItems: 'flex-start',
  },
  welcomeMargin: {
    paddingBottom: 40,
    alignSelf: 'stretch',
  },
  welcomeContainer: {
    flexDirection: 'column',
    alignItems: 'flex-start',
    gap: 8,
  },
  headingContainer: {
    flexDirection: 'column',
    alignItems: 'center',
    alignSelf: 'stretch',
  },
  welcomeTitle: {
    fontFamily: 'System',
    fontWeight: '700',
    fontSize: 30,
    lineHeight: 36,
    color: '#FFFFFF',
    textAlign: 'center',
    alignSelf: 'stretch',
  },
  subtitleContainer: {
    flexDirection: 'column',
    alignItems: 'center',
    alignSelf: 'stretch',
  },
  subtitle: {
    fontFamily: 'System',
    fontWeight: '400',
    fontSize: 16,
    lineHeight: 24,
    color: '#94A3B8',
    textAlign: 'center',
    alignSelf: 'stretch',
  },
  formMargin: {
    paddingBottom: 16,
    alignSelf: 'stretch',
  },
  form: {
    height: 260,
    position: 'relative',
  },
  emailInputContainer: {
    position: 'absolute',
    height: 56,
    left: 0,
    right: 0,
    top: 0,
  },
  passwordInputContainer: {
    position: 'absolute',
    height: 56,
    left: 0,
    right: 0,
    top: 80,
  },
  textInput: {
    flex: 1,
    height: 56,
    backgroundColor: '#1A2333',
    borderWidth: 1,
    borderColor: '#334155',
    borderRadius: 8,
    paddingHorizontal: 17,
    paddingVertical: 18.5,
    fontFamily: 'System',
    fontWeight: '400',
    fontSize: 16,
    lineHeight: 19,
    color: '#FFFFFF',
  },
  optionsContainer: {
    position: 'absolute',
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    left: 0,
    right: 0,
    top: 160,
    height: 20,
  },
  rememberMeContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 8,
  },
  checkbox: {
    width: 16,
    height: 16,
    backgroundColor: '#334155',
    borderWidth: 1,
    borderColor: '#475569',
    borderRadius: 4,
    justifyContent: 'center',
    alignItems: 'center',
  },
  checkboxChecked: {
    backgroundColor: '#2563EB',
    borderColor: '#2563EB',
  },
  checkmark: {
    color: '#FFFFFF',
    fontSize: 10,
    fontWeight: 'bold',
  },
  rememberMeLabel: {
    fontFamily: 'System',
    fontWeight: '400',
    fontSize: 14,
    lineHeight: 20,
    color: '#CBD5E1',
  },
  forgotPasswordContainer: {
    flexDirection: 'column',
    alignItems: 'flex-start',
  },
  forgotPasswordText: {
    fontFamily: 'System',
    fontWeight: '500',
    fontSize: 14,
    lineHeight: 20,
    color: '#60A5FA',
  },
  signInButton: {
    position: 'absolute',
    height: 56,
    left: 0,
    right: 0,
    top: 204,
    backgroundColor: '#2563EB',
    borderRadius: 8,
    justifyContent: 'center',
    alignItems: 'center',
  },
  signInButtonText: {
    fontFamily: 'System',
    fontWeight: '700',
    fontSize: 16,
    lineHeight: 24,
    color: '#FFFFFF',
    textAlign: 'center',
  },
  separatorMargin: {
    paddingVertical: 32,
    alignSelf: 'stretch',
  },
  separatorContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingBottom: 0,
    alignSelf: 'stretch',
  },
  separatorLine: {
    flex: 1,
    height: 1,
    borderTopWidth: 1,
    borderTopColor: '#334155',
  },
  separatorTextContainer: {
    paddingHorizontal: 16,
  },
  separatorText: {
    fontFamily: 'System',
    fontWeight: '400',
    fontSize: 14,
    lineHeight: 20,
    color: '#64748B',
  },
  createAccountContainer: {
    flexDirection: 'row',
    justifyContent: 'center',
    alignItems: 'flex-start',
    alignSelf: 'stretch',
    marginTop: 20,
  },
  newUserText: {
    fontFamily: 'System',
    fontWeight: '400',
    fontSize: 14,
    lineHeight: 20,
    color: '#94A3B8',
    textAlign: 'center',
  },
  createAccountText: {
    fontFamily: 'System',
    fontWeight: '500',
    fontSize: 14,
    lineHeight: 20,
    color: '#60A5FA',
    textAlign: 'center',
  },
  footerMargin: {
    paddingTop: 40,
    alignSelf: 'stretch',
  },
  footer: {
    flexDirection: 'column',
    alignItems: 'center',
    alignSelf: 'stretch',
  },
  footerText: {
    fontFamily: 'System',
    fontWeight: '400',
    fontSize: 12,
    lineHeight: 16,
    color: '#64748B',
    textAlign: 'center',
    alignSelf: 'stretch',
  },
});

export default SignIn;