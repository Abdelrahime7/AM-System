
import React from 'react';
import {
  View,
  Text,
  ScrollView,
  TextInput,
  TouchableOpacity,
  Dimensions,
  StatusBar,
  SafeAreaView,
  Image
} from 'react-native';
import { Ionicons } from '@expo/vector-icons';

const { width: screenWidth, height: screenHeight } = Dimensions.get('window');

const AffiliatesScreen = () => {
  const requestsData = [
    { id: 1, name: 'Yasmine Al-Farsi', role: 'Admin', avatar: 'https://images.unsplash.com/photo-1494790108755-2616b612b786?w=70&h=70&fit=crop&crop=face' },
    { id: 2, name: 'Yasmine Al-Farsi', role: 'Affiliate', avatar: 'https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?w=70&h=70&fit=crop&crop=face' },
    { id: 3, name: 'Faisal Al-Harbi', role: 'Affiliate', avatar: 'https://images.unsplash.com/photo-1472099645785-5658abf4ff4e?w=70&h=70&fit=crop&crop=face' },
    { id: 4, name: 'Faisal Al-Harbi', role: 'Affiliate', avatar: 'https://images.unsplash.com/photo-1500648767791-00dcc994a43e?w=70&h=70&fit=crop&crop=face' },
  ];

  const currentUsers = [
    { id: 1, name: 'Omar Hassan', role: 'Affiliate', avatar: 'https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?w=56&h=56&fit=crop&crop=face' },
    { id: 2, name: 'Layla Al-Mousa', role: 'Call Center Agent', avatar: 'https://images.unsplash.com/photo-1494790108755-2616b612b786?w=56&h=56&fit=crop&crop=face' },
    { id: 3, name: 'Khaled Al-Rashid', role: 'Affiliate', avatar: 'https://images.unsplash.com/photo-1472099645785-5658abf4ff4e?w=56&h=56&fit=crop&crop=face' },
    { id: 4, name: 'Fatima Al-Zahrani', role: 'Assistant Admin', avatar: 'https://images.unsplash.com/photo-1438761681033-6461ffad8d80?w=56&h=56&fit=crop&crop=face' },
    { id: 5, name: 'Abdullah Al-Salem', role: 'Admin', avatar: 'https://images.unsplash.com/photo-1500648767791-00dcc994a43e?w=56&h=56&fit=crop&crop=face' },
  ];

  const RequestCard = ({ item }) => (
    <View style={{
      flexDirection: 'row',
      justifyContent: 'space-between',
      alignItems: 'flex-start',
      paddingHorizontal: 16,
      paddingVertical: 12,
      backgroundColor: '#121721',
      gap: 16,
    }}>
      <View style={{
        flexDirection: 'row',
        alignItems: 'flex-start',
        gap: 16,
        flex: 1,
      }}>
        <Image
          source={{ uri: item.avatar }}
          style={{
            width: 70,
            height: 70,
            borderRadius: 35,
            backgroundColor: '#C8F1FF',
          }}
        />
        <View style={{
          justifyContent: 'center',
          flex: 1,
        }}>
          <Text style={{
            fontFamily: 'Inter',
            fontWeight: '500',
            fontSize: 16,
            lineHeight: 24,
            color: '#FFFFFF',
            marginBottom: 2,
          }}>
            {item.name}
          </Text>
          <Text style={{
            fontFamily: 'Inter',
            fontWeight: '400',
            fontSize: 14,
            lineHeight: 21,
            color: '#94A6C7',
          }}>
            Role: {item.role}
          </Text>
        </View>
      </View>
      
      <View style={{
        alignItems: 'flex-start',
        gap: 6,
        width: 84,
      }}>
        <TouchableOpacity style={{
          paddingHorizontal: 16,
          paddingVertical: 6,
          backgroundColor: 'rgba(34, 197, 94, 0.2)',
          borderRadius: 8,
          width: '100%',
          alignItems: 'center',
        }}>
          <Text style={{
            fontFamily: 'Inter',
            fontWeight: '500',
            fontSize: 14,
            lineHeight: 21,
            color: '#22C55E',
          }}>
            Accept
          </Text>
        </TouchableOpacity>
        
        <TouchableOpacity style={{
          paddingHorizontal: 16,
          paddingVertical: 6,
          backgroundColor: 'rgba(239, 68, 68, 0.2)',
          borderRadius: 8,
          width: '100%',
          alignItems: 'center',
        }}>
          <Text style={{
            fontFamily: 'Inter',
            fontWeight: '500',
            fontSize: 14,
            lineHeight: 21,
            color: '#EF4444',
          }}>
            Decline
          </Text>
        </TouchableOpacity>
      </View>
    </View>
  );

  const UserCard = ({ item }) => (
    <View style={{
      flexDirection: 'row',
      alignItems: 'center',
      paddingHorizontal: 16,
      paddingVertical: 8,
      backgroundColor: '#121721',
      gap: 16,
      minHeight: 72,
    }}>
      <Image
        source={{ uri: item.avatar }}
        style={{
          width: 56,
          height: 56,
          borderRadius: 28,
          backgroundColor: '#C8F1FF',
        }}
      />
      <View style={{
        justifyContent: 'center',
        flex: 1,
      }}>
        <Text style={{
          fontFamily: 'Inter',
          fontWeight: '500',
          fontSize: 16,
          lineHeight: 24,
          color: '#FFFFFF',
          marginBottom: 2,
        }}>
          {item.name}
        </Text>
        <Text style={{
          fontFamily: 'Inter',
          fontWeight: '400',
          fontSize: 14,
          lineHeight: 21,
          color: '#94A6C7',
        }}>
          {item.role}
        </Text>
      </View>
    </View>
  );

  return (
    <SafeAreaView style={{
      flex: 1,
      backgroundColor: '#121721',
      width: screenWidth,
    }}>
      <StatusBar barStyle="light-content" backgroundColor="#121721" />
      
      <ScrollView 
        style={{
          flex: 1,
          backgroundColor: '#121721',
        }}
        contentContainerStyle={{
          minHeight: screenHeight - StatusBar.currentHeight,
        }}
      >
        {/* Header */}
        <View style={{
          flexDirection: 'row',
          justifyContent: 'space-between',
          alignItems: 'center',
          paddingHorizontal: 16,
          paddingVertical: 16,
          paddingBottom: 8,
          backgroundColor: '#121721',
        }}>
          <View style={{
            alignItems: 'center',
            paddingLeft: 48,
            flex: 1,
          }}>
            <Text style={{
              fontFamily: 'Inter',
              fontWeight: '700',
              fontSize: 18,
              lineHeight: 23,
              color: '#FFFFFF',
              textAlign: 'center',
            }}>
              Affiliates
            </Text>
          </View>
          
          <TouchableOpacity style={{
            padding: 12,
            borderRadius: 8,
          }}>
            <Ionicons name="menu" size={24} color="#FFFFFF" />
          </TouchableOpacity>
        </View>

        {/* Search Bar */}
        <View style={{
          paddingHorizontal: 16,
          paddingVertical: 12,
        }}>
          <View style={{
            flexDirection: 'row',
            borderRadius: 8,
            overflow: 'hidden',
          }}>
            <View style={{
              backgroundColor: '#243347',
              paddingLeft: 16,
              justifyContent: 'center',
              alignItems: 'center',
              width: 40,
              borderTopLeftRadius: 8,
              borderBottomLeftRadius: 8,
            }}>
              <Ionicons name="search" size={24} color="#94A6C7" />
            </View>
            
            <TextInput
              placeholder="Search affiliates"
              placeholderTextColor="#94A6C7"
              style={{
                flex: 1,
                backgroundColor: '#243347',
                paddingHorizontal: 16,
                paddingVertical: 12,
                fontFamily: 'Inter',
                fontSize: 16,
                color: '#FFFFFF',
                borderTopRightRadius: 8,
                borderBottomRightRadius: 8,
              }}
            />
          </View>
        </View>

        {/* Filter Buttons */}
        <View style={{
          flexDirection: 'row',
          paddingHorizontal: 12,
          paddingVertical: 12,
          gap: 12,
        }}>
          <TouchableOpacity style={{
            flexDirection: 'row',
            alignItems: 'center',
            paddingLeft: 16,
            paddingRight: 8,
            paddingVertical: 6,
            backgroundColor: '#243347',
            borderRadius: 8,
            gap: 8,
          }}>
            <Text style={{
              fontFamily: 'Inter',
              fontWeight: '500',
              fontSize: 14,
              color: '#FFFFFF',
            }}>
              Role
            </Text>
            <Ionicons name="chevron-down" size={20} color="#FFFFFF" />
          </TouchableOpacity>
          
          <TouchableOpacity style={{
            flexDirection: 'row',
            alignItems: 'center',
            paddingLeft: 16,
            paddingRight: 8,
            paddingVertical: 6,
            backgroundColor: '#243347',
            borderRadius: 8,
            gap: 8,
          }}>
            <Text style={{
              fontFamily: 'Inter',
              fontWeight: '500',
              fontSize: 14,
              color: '#FFFFFF',
            }}>
              Status
            </Text>
            <Ionicons name="chevron-down" size={20} color="#FFFFFF" />
          </TouchableOpacity>
        </View>

        {/* Requests Section */}
        <View style={{
          paddingHorizontal: 16,
          paddingTop: 20,
          paddingBottom: 12,
        }}>
          <Text style={{
            fontFamily: 'Inter',
            fontWeight: '700',
            fontSize: 22,
            lineHeight: 28,
            color: '#FFFFFF',
          }}>
            Requests
          </Text>
        </View>

        {/* Requests List */}
        {requestsData.map((item, index) => (
          <RequestCard key={item.id} item={item} />
        ))}

        {/* Current Users Section */}
        <View style={{
          paddingHorizontal: 16,
          paddingTop: 20,
          paddingBottom: 12,
        }}>
          <Text style={{
            fontFamily: 'Inter',
            fontWeight: '700',
            fontSize: 22,
            lineHeight: 28,
            color: '#FFFFFF',
          }}>
            Current Users
          </Text>
        </View>

        {/* Current Users List */}
        {currentUsers.map((item, index) => (
          <UserCard key={item.id} item={item} />
        ))}
      </ScrollView>

      {/* Bottom Navigation */}
      <View style={{
        height: 65,
        backgroundColor: '#192433',
        shadowColor: '#000',
        shadowOffset: { width: 0, height: -3 },
        shadowOpacity: 0.25,
        shadowRadius: 6,
        elevation: 10,
        flexDirection: 'row',
        alignItems: 'center',
        paddingHorizontal: 16,
      }}>
        <TouchableOpacity style={{
          flex: 1,
          alignItems: 'center',
          paddingVertical: 9,
        }}>
          <Ionicons name="home" size={24} color="rgba(255, 255, 255, 0.3)" />
          <Text style={{
            fontFamily: 'Inter',
            fontWeight: '500',
            fontSize: 12,
            color: 'rgba(255, 255, 255, 0.3)',
            marginTop: 4,
          }}>
            Home
          </Text>
        </TouchableOpacity>
        
        <TouchableOpacity style={{
          flex: 1,
          alignItems: 'center',
          paddingVertical: 9,
        }}>
          <Ionicons name="people" size={24} color="#FFFFFF" />
          <Text style={{
            fontFamily: 'Inter',
            fontWeight: '500',
            fontSize: 12,
            color: '#FFFFFF',
            marginTop: 4,
          }}>
            Users
          </Text>
        </TouchableOpacity>
        
        <TouchableOpacity style={{
          flex: 1,
          alignItems: 'center',
          paddingVertical: 9,
        }}>
          <Ionicons name="storefront" size={24} color="rgba(255, 255, 255, 0.3)" />
          <Text style={{
            fontFamily: 'Inter',
            fontWeight: '500',
            fontSize: 12,
            color: 'rgba(255, 255, 255, 0.3)',
            marginTop: 4,
          }}>
            Products
          </Text>
        </TouchableOpacity>
        
        <TouchableOpacity style={{
          flex: 1,
          alignItems: 'center',
          paddingVertical: 9,
        }}>
          <Ionicons name="card" size={24} color="rgba(255, 255, 255, 0.3)" />
          <Text style={{
            fontFamily: 'Inter',
            fontWeight: '500',
            fontSize: 12,
            color: 'rgba(255, 255, 255, 0.3)',
            marginTop: 4,
          }}>
            Finance
          </Text>
        </TouchableOpacity>
        
        <TouchableOpacity style={{
          flex: 1,
          alignItems: 'center',
          paddingVertical: 9,
        }}>
          <Ionicons name="analytics" size={24} color="rgba(255, 255, 255, 0.3)" />
          <Text style={{
            fontFamily: 'Inter',
            fontWeight: '500',
            fontSize: 12,
            color: 'rgba(255, 255, 255, 0.3)',
            marginTop: 4,
          }}>
            Analytics
          </Text>
        </TouchableOpacity>
      </View>
    </SafeAreaView>
  );
};

export default AffiliatesScreen;