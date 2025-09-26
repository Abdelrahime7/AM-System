import React, { useState } from 'react';
import {
  View,
  Text,
  StyleSheet,
  ScrollView,
  TouchableOpacity,
  SafeAreaView,
  StatusBar,
  Image,
  Alert,
  Dimensions,
} from 'react-native';
import { Ionicons } from '@expo/vector-icons';

const { width } = Dimensions.get('window');

const ProductDetailsScreen = () => {
  const [activeImageIndex, setActiveImageIndex] = useState(0);
  
  const productData = {
    title: 'Premium Organic Coffee Beans',
    description: 'Sourced from the finest farms, our organic coffee beans offer a rich, aromatic experience with every brew. Perfect for coffee enthusiasts seeking quality and sustainability.',
    price: 19.99,
    commission: 2.00,
    commissionRate: 10,
    totalOrders: 1234,
    status: 'Active',
    images: [
      { uri: 'https://png.pngtree.com/png-clipart/20200701/original/pngtree-cereal-oil-soybean-oil-png-image_5401940.jpg' },
      { uri: 'https://png.pngtree.com/png-clipart/20200701/original/pngtree-cereal-oil-soybean-oil-png-image_5401940.jpg' },
      { uri: 'https://png.pngtree.com/png-clipart/20200701/original/pngtree-cereal-oil-soybean-oil-png-image_5401940.jpg' },
      { uri: 'https://png.pngtree.com/png-clipart/20200701/original/pngtree-cereal-oil-soybean-oil-png-image_5401940.jpg' },
    ]
  };

  const handleBackPress = () => {
    // Navigate back to previous screen
    console.log('Back pressed');
  };

  const handleAddToOrder = () => {
    Alert.alert(
      'Added to Order',
      `${productData.title} has been added to your order.`,
      [{ text: 'OK' }]
    );
  };

  const handleDownloadImages = () => {
    Alert.alert(
      'Download Images',
      'Product images will be downloaded to your device.',
      [
        { text: 'Cancel', style: 'cancel' },
        { text: 'Download', onPress: () => console.log('Downloading images...') }
      ]
    );
  };

  const ImageThumbnail = ({ image, index, isActive, onPress }) => (
    <TouchableOpacity 
      style={[styles.thumbnail, isActive && styles.activeThumbnail]}
      onPress={() => onPress(index)}
    >
        <Image 
      source={{uri:'https://png.pngtree.com/png-clipart/20200701/original/pngtree-cereal-oil-soybean-oil-png-image_5401940.jpg'}} 
      style={styles.thumbnailImage} 
      resizeMode="cover"
    />

      <View style={styles.thumbnailPlaceholder} />
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
        <Text style={styles.headerTitle}>Product Details</Text>
      </View>

      <ScrollView 
        style={styles.scrollView}
        showsVerticalScrollIndicator={false}
        contentContainerStyle={styles.scrollContent}
      >
        {/* Main Product Image */}
<View style={styles.mainImageContainer}>
  <Image 
    source={productData.images[activeImageIndex]} 
    style={styles.mainImage} 
    resizeMode="contain"
  />
</View>
        {/* Image Thumbnails */}
        <View style={styles.thumbnailsContainer}>
          {productData.images.map((image, index) => (
            <ImageThumbnail
              key={index}
              image={image}
              index={index}
              isActive={index === activeImageIndex}
              onPress={setActiveImageIndex}
            />
          ))}
        </View>

        {/* Download Images Button */}
        <View style={styles.downloadButtonContainer}>
          <TouchableOpacity 
            style={styles.downloadButton}
            onPress={handleDownloadImages}
          >
            <Text style={styles.downloadButtonText}>Download Images</Text>
          </TouchableOpacity>

          {/* Status Badge */}
          <View style={styles.statusBadge}>
            <Text style={styles.statusText}>{productData.status}</Text>
          </View>
        </View>

        {/* Product Information */}
        <View style={styles.productInfo}>
          <Text style={styles.productTitle}>{productData.title}</Text>
          <Text style={styles.totalOrders}>Total Orders: {productData.totalOrders.toLocaleString()}</Text>
          <Text style={styles.productDescription}>{productData.description}</Text>
        </View>

        {/* Price and Commission */}
        <View style={styles.priceSection}>
          <View style={styles.priceColumn}>
            <Text style={styles.sectionTitle}>Price</Text>
            <Text style={styles.price}>${productData.price.toFixed(2)}</Text>
          </View>
          
          <View style={styles.commissionColumn}>
            <Text style={styles.sectionTitle}>Affiliate Commission</Text>
            <Text style={styles.commission}>
              {productData.commissionRate}% (${productData.commission.toFixed(2)})
            </Text>
          </View>
        </View>
      </ScrollView>

      {/* Add to Order Button */}
      <View style={styles.addToOrderContainer}>
        <TouchableOpacity 
          style={styles.addToOrderButton}
          onPress={handleAddToOrder}
        >
          <Text style={styles.addToOrderText}>Add to Order</Text>
        </TouchableOpacity>
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
    lineHeight: 22,
  },
  scrollView: {
    flex: 1,
  },
  scrollContent: {
    paddingBottom: 20,
  },
  mainImageContainer: {
    marginHorizontal: 16,
    marginTop: 16,
    height: 238,
    borderRadius: 12,
    overflow: 'hidden',
  },
  mainImagePlaceholder: {
    width: '100%',
    height: '100%',
    backgroundColor: '#233148',
  },
  thumbnailsContainer: {
    flexDirection: 'row',
    paddingHorizontal: 16,
    marginTop: 16,
    justifyContent: 'space-between',
  },
  thumbnail: {
    width: 74,
    height: 82,
    borderRadius: 8,
    overflow: 'hidden',
    opacity: 0.6,
  },
  activeThumbnail: {
    opacity: 1,
    borderWidth: 2,
    borderColor: '#2160F2',
  },
  thumbnailPlaceholder: {
    width: '100%',
    height: '100%',
    backgroundColor: '#233148',
  },
  downloadButtonContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingHorizontal: 16,
    marginTop: 16,
  },
  downloadButton: {
    backgroundColor: '#233148',
    paddingHorizontal: 24,
    paddingVertical: 12,
    borderRadius: 8,
    height: 40,
    justifyContent: 'center',
  },
  downloadButtonText: {
    fontSize: 14,
    fontWeight: '700',
    color: '#FFFFFF',
    letterSpacing: 0.21,
    textAlign: 'center',
    lineHeight: 21,
  },
  statusBadge: {
    backgroundColor: 'rgba(34, 197, 94, 0.1)',
    paddingHorizontal: 16,
    paddingVertical: 7,
    borderRadius: 15,
    height: 30,
    justifyContent: 'center',
  },
  statusText: {
    fontSize: 16,
    fontWeight: '600',
    color: '#4ADE80',
    lineHeight: 16,
  },
  productInfo: {
    paddingHorizontal: 16,
    marginTop: 24,
  },
  productTitle: {
    fontSize: 22,
    fontWeight: '700',
    color: '#FFFFFF',
    lineHeight: 28,
    letterSpacing: -0.33,
    marginBottom: 8,
  },
  totalOrders: {
    fontSize: 14,
    fontWeight: '400',
    color: '#FFFFFF',
    lineHeight: 21,
    letterSpacing: 0.21,
    textAlign: 'center',
    marginBottom: 16,
  },
  productDescription: {
    fontSize: 16,
    fontWeight: '400',
    color: '#FFFFFF',
    lineHeight: 24,
  },
  priceSection: {
    flexDirection: 'row',
    paddingHorizontal: 16,
    marginTop: 32,
    justifyContent: 'space-between',
  },
  priceColumn: {
    flex: 1,
    paddingRight: 16,
  },
  commissionColumn: {
    flex: 1,
    paddingLeft: 16,
  },
  sectionTitle: {
    fontSize: 18,
    fontWeight: '700',
    color: '#FFFFFF',
    lineHeight: 22,
    letterSpacing: -0.27,
    marginBottom: 8,
  },
  price: {
    fontSize: 16,
    fontWeight: '400',
    color: '#FFFFFF',
    lineHeight: 24,
  },
  commission: {
    fontSize: 16,
    fontWeight: '400',
    color: '#FFFFFF',
    lineHeight: 24,
  },
  addToOrderContainer: {
    paddingHorizontal: 16,
    paddingVertical: 16,
  },
  addToOrderButton: {
    backgroundColor: '#2160F2',
    height: 48,
    borderRadius: 8,
    alignItems: 'center',
    justifyContent: 'center',
  },
  addToOrderText: {
    fontSize: 16,
    fontWeight: '700',
    color: '#FFFFFF',
    lineHeight: 24,
    letterSpacing: 0.24,
  },
  mainImage: {
  width: '100%',
  height: '100%',
},
thumbnailImage: {
  width: '100%',
  height: '100%',
  borderRadius: 8,
},

});

export default ProductDetailsScreen;