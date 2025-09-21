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

const SignUp = () => {
  const [fullName, setFullName] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [selectedRole, setSelectedRole] = useState('');
  const [showRoleDropdown, setShowRoleDropdown] = useState(false);
    const navigation = useNavigation();

  const roles = ['Affiliate', 'Merchant', 'Administrator', 'Content Creator'];

  const handleCreateAccount = () => {
    console.log('Create account pressed');
    console.log('Full Name:', fullName);
    console.log('Email:', email);

    
    console.log('Password:', password);
    console.log('Confirm Password:', confirmPassword);
    console.log('Selected Role:', selectedRole);
    // Add your sign up logic here
  };

  const handleSignIn = () => {
    console.log('Sign in pressed');

navigation.navigate("signin")
  };

  const toggleLanguage = () => {
    console.log('Language toggle pressed');
    // Add your language toggle logic here
  };

  const handleRoleSelect = (role) => {
    setSelectedRole(role);
    setShowRoleDropdown(false);
  };

  const toggleRoleDropdown = () => {
    setShowRoleDropdown(!showRoleDropdown);
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
                  <Text style={styles.welcomeTitle}>Create Account</Text>
                </View>
                <View style={styles.subtitleContainer}>
                  <Text style={styles.subtitle}>Join our affiliate network</Text>
                </View>
              </View>
            </View>

            {/* Form Section */}
            <View style={styles.formMargin}>
              <View style={styles.form}>
                {/* Full Name Input */}
                <View style={styles.fullNameInputContainer}>
                  <TextInput
                    style={styles.textInput}
                    placeholder="Full Name"
                    placeholderTextColor="#9CA3AF"
                    value={fullName}
                    onChangeText={setFullName}
                    autoCapitalize="words"
                    autoCorrect={false}
                  />
                </View>

                {/* Email Input */}
                <View style={styles.emailInputContainer}>
                  <TextInput
                    style={styles.textInput}
                    placeholder="Email"
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

                {/* Confirm Password Input */}
                <View style={styles.confirmPasswordInputContainer}>
                  <TextInput
                    style={styles.textInput}
                    placeholder="Confirm Password"
                    placeholderTextColor="#9CA3AF"
                    value={confirmPassword}
                    onChangeText={setConfirmPassword}
                    secureTextEntry={true}
                    autoCapitalize="none"
                    autoCorrect={false}
                  />
                </View>

                {/* Role Selection Dropdown */}
                <View style={styles.roleInputContainer}>
                  <TouchableOpacity 
                    style={styles.roleSelector}
                    onPress={toggleRoleDropdown}
                    activeOpacity={0.8}
                  >
                    <Text style={[
                      styles.rolePlaceholder,
                      selectedRole && styles.roleSelected
                    ]}>
                      {selectedRole || 'Select your role'}
                    </Text>
                    <View style={styles.dropdownIcon}>
                      <Text style={styles.dropdownArrow}>▼</Text>
                    </View>
                  </TouchableOpacity>
                  
                  {showRoleDropdown && (
                    <View style={styles.dropdownMenu}>
                      {roles.map((role, index) => (
                        <TouchableOpacity
                          key={index}
                          style={[
                            styles.dropdownItem,
                            index === roles.length - 1 && styles.lastDropdownItem
                          ]}
                          onPress={() => handleRoleSelect(role)}
                          activeOpacity={0.8}
                        >
                          <Text style={styles.dropdownItemText}>{role}</Text>
                        </TouchableOpacity>
                      ))}
                    </View>
                  )}
                </View>

                {/* Create Account Button */}
                <TouchableOpacity 
                  style={styles.createAccountButton} 
                  onPress={handleCreateAccount}
                  activeOpacity={0.9}
                >
                  <Text style={styles.createAccountButtonText}>Create Account</Text>
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

              {/* Sign In Link */}
              <View style={styles.signInContainer}>
                <Text style={styles.alreadyHaveAccountText}>Already have an account? </Text>
                <TouchableOpacity 
                  onPress={handleSignIn}
                  activeOpacity={0.8}
                >
                  <Text style={styles.signInText}>Sign In</Text>
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
    paddingBottom: 76,
  },
  headerMargin: {
    paddingBottom: 32,
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
    justifyContent: 'center',
    alignItems: 'flex-start',
  },
  welcomeMargin: {
    paddingBottom: 32,
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
    height: 436,
    position: 'relative',
  },
  fullNameInputContainer: {
    position: 'absolute',
    height: 56,
    left: 0,
    right: 0,
    top: 0,
  },
  emailInputContainer: {
    position: 'absolute',
    height: 56,
    left: 0,
    right: 0,
    top: 72,
  },
  passwordInputContainer: {
    position: 'absolute',
    height: 56,
    left: 0,
    right: 0,
    top: 144,
  },
  confirmPasswordInputContainer: {
    position: 'absolute',
    height: 56,
    left: 0,
    right: 0,
    top: 216,
  },
  roleInputContainer: {
    position: 'absolute',
    left: 0,
    right: 0,
    top: 288,
    zIndex: 1000,
  },
  textInput: {
    flex: 1,
    height: 56,
    backgroundColor: '#1A2333',
    borderWidth: 1,
    borderColor: '#334155',
    borderRadius: 8,
    paddingHorizontal: 24,
    paddingVertical: 18.5,
    fontFamily: 'System',
    fontWeight: '400',
    fontSize: 16,
    lineHeight: 19,
    color: '#FFFFFF',
  },
  roleSelector: {
    height: 56,
    backgroundColor: '#1A2333',
    borderWidth: 1,
    borderColor: '#334155',
    borderRadius: 8,
    paddingHorizontal: 24,
    paddingVertical: 18.5,
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
  },
  rolePlaceholder: {
    fontFamily: 'System',
    fontWeight: '400',
    fontSize: 16,
    lineHeight: 24,
    color: '#9CA3AF',
  },
  roleSelected: {
    color: '#FFFFFF',
  },
  dropdownIcon: {
    width: 24,
    height: 24,
    justifyContent: 'center',
    alignItems: 'center',
  },
  dropdownArrow: {
    fontSize: 12,
    color: '#6B7280',
  },
  dropdownMenu: {
    backgroundColor: '#1A2333',
    borderWidth: 1,
    borderColor: '#334155',
    borderRadius: 8,
    marginTop: 4,
    maxHeight: 200,
  },
  dropdownItem: {
    paddingHorizontal: 24,
    paddingVertical: 16,
    borderBottomWidth: 1,
    borderBottomColor: '#334155',
  },
  lastDropdownItem: {
    borderBottomWidth: 0,
  },
  dropdownItemText: {
    fontFamily: 'System',
    fontWeight: '400',
    fontSize: 16,
    color: '#FFFFFF',
  },
  createAccountButton: {
    position: 'absolute',
    height: 56,
    left: 0,
    right: 0,
    top: 380,
    backgroundColor: '#2563EB',
    borderRadius: 8,
    justifyContent: 'center',
    alignItems: 'center',
  },
  createAccountButtonText: {
    fontFamily: 'System',
    fontWeight: '700',
    fontSize: 16,
    lineHeight: 24,
    color: '#FFFFFF',
    textAlign: 'center',
  },
  separatorMargin: {
    paddingVertical: 24,
    alignSelf: 'stretch',
  },
  separatorContainer: {
    flexDirection: 'row',
    alignItems: 'center',
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
  signInContainer: {
    flexDirection: 'row',
    justifyContent: 'center',
    alignItems: 'flex-start',
    alignSelf: 'stretch',
    marginTop: 20,
  },
  alreadyHaveAccountText: {
    fontFamily: 'System',
    fontWeight: '400',
    fontSize: 14,
    lineHeight: 20,
    color: '#94A3B8',
    textAlign: 'center',
  },
  signInText: {
    fontFamily: 'System',
    fontWeight: '500',
    fontSize: 14,
    lineHeight: 20,
    color: '#60A5FA',
    textAlign: 'center',
  },
  footerMargin: {
    paddingTop: 32,
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

export default SignUp;