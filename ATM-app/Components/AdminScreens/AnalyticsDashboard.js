import React from 'react';
import { View, Text, ScrollView, StyleSheet, Dimensions } from 'react-native';
import { Ionicons } from '@expo/vector-icons';

const { width } = Dimensions.get('window');

const AnalyticsDashboard = () => {
  const MetricCard = ({ title, value, change, isWide = false }) => (
    <View style={[styles.metricCard, isWide && styles.wideCard]}>
      <Text style={styles.metricTitle}>{title}</Text>
      <Text style={styles.metricValue}>{value}</Text>
      <Text style={[styles.metricChange, change.startsWith('-') && styles.negativeChange]}>
        {change}
      </Text>
    </View>
  );

  const BarChart = ({ data, labels }) => (
    <View style={styles.chartContainer}>
      <View style={styles.barsContainer}>
        {data.map((height, index) => (
          <View key={index} style={styles.barColumn}>
            <View style={[styles.bar, { height: `${height}%` }]} />
          </View>
        ))}
      </View>
      <View style={styles.labelsContainer}>
        {labels.map((label, index) => (
          <Text key={index} style={styles.chartLabel}>{label}</Text>
        ))}
      </View>
    </View>
  );

  const ProductBar = ({ product, width }) => (
    <View style={styles.productRow}>
      <Text style={styles.productName}>{product}</Text>
      <View style={styles.productBarContainer}>
        <View style={[styles.productBar, { width: `${width}%` }]} />
      </View>
    </View>
  );

  return (
    <View style={styles.container}>
      <ScrollView style={styles.scrollContainer} showsVerticalScrollIndicator={false}>
        {/* Header */}
        <View style={styles.header}>
          <Ionicons name="chevron-back" size={24} color="#fff" />
          <Text style={styles.headerTitle}>Analytics</Text>
          <View style={{ width: 24 }} />
        </View>

        {/* Key Metrics Section */}
        <View style={styles.section}>
          <Text style={styles.sectionTitle}>Key Metrics</Text>
          <View style={styles.metricsGrid}>
            <MetricCard title="Total Sales" value="$125,000" change="+15%" />
            <MetricCard title="Active Affiliates" value="500" change="+5%" />
            <MetricCard title="Top Products" value="Electronics" change="+20%" isWide />
          </View>
        </View>

        {/* Sales Trends Section */}
        <View style={styles.section}>
          <Text style={styles.sectionTitle}>Sales Trends</Text>
          <View style={styles.trendsContainer}>
            <Text style={styles.trendsValue}>$125,000</Text>
            <View style={styles.trendsSubInfo}>
              <Text style={styles.trendsSubText}>Last 12 Months</Text>
              <Text style={styles.positiveChange}>+15%</Text>
            </View>
            
            {/* Line Chart Placeholder */}
            <View style={styles.lineChartContainer}>
              <View style={styles.lineChart}>
                <View style={styles.chartLine} />
                <View style={styles.chartArea} />
              </View>
              <View style={styles.monthLabels}>
                {['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul'].map((month, index) => (
                  <Text key={index} style={styles.monthLabel}>{month}</Text>
                ))}
              </View>
            </View>
          </View>
        </View>

        {/* Top 5 Performing Products */}
        <View style={styles.section}>
          <Text style={styles.sectionTitle}>Top 5 Performing Products</Text>
          <View style={styles.productsContainer}>
            <View style={styles.productsSubInfo}>
              <Text style={styles.trendsSubText}>Last Month</Text>
              <Text style={styles.positiveChange}>+10%</Text>
            </View>
            <View style={styles.productsList}>
              <ProductBar product="Product A" width={80} />
              <ProductBar product="Product B" width={20} />
              <ProductBar product="Product C" width={100} />
              <ProductBar product="Product D" width={80} />
              <ProductBar product="Product E" width={100} />
            </View>
          </View>
        </View>

        {/* Top 5 Affiliates */}
        <View style={styles.section}>
          <Text style={styles.sectionTitle}>Top 5 Affiliates</Text>
          <View style={styles.affiliatesContainer}>
            <View style={styles.productsSubInfo}>
              <Text style={styles.trendsSubText}>Last Month</Text>
              <Text style={styles.positiveChange}>+5%</Text>
            </View>
            <BarChart 
              data={[60, 35, 85, 15, 80]} 
              labels={['Affiliate 1', 'Affiliate 2', 'Affiliate 3', 'Affiliate 4', 'Affiliate 5']}
            />
          </View>
        </View>

        {/* Average Delivery Time */}
        <View style={styles.section}>
          <Text style={styles.sectionTitle}>Average Delivery Time</Text>
          <View style={styles.deliveryContainer}>
            <View style={styles.productsSubInfo}>
              <Text style={styles.trendsSubText}>Last Month</Text>
              <Text style={styles.negativeChange}>-2%</Text>
            </View>
            <BarChart 
              data={[30, 90, 70, 15, 65]} 
              labels={['1-2 Days', '2-3 Days', '3-4 Days', '4-5 Days', '5+ Days']}
            />
          </View>
        </View>
      </ScrollView>

      {/* Bottom Navigation */}
      <View style={styles.bottomNav}>
        <View style={styles.navItem}>
          <Ionicons name="home-outline" size={24} color="rgba(255, 255, 255, 0.3)" />
          <Text style={styles.navLabel}>Home</Text>
        </View>
        <View style={styles.navItem}>
          <Ionicons name="people-outline" size={24} color="rgba(255, 255, 255, 0.3)" />
          <Text style={styles.navLabel}>Users</Text>
        </View>
        <View style={styles.navItem}>
          <Ionicons name="cube-outline" size={24} color="rgba(255, 255, 255, 0.3)" />
          <Text style={styles.navLabel}>Products</Text>
        </View>
        <View style={styles.navItem}>
          <Ionicons name="card-outline" size={24} color="rgba(255, 255, 255, 0.3)" />
          <Text style={styles.navLabel}>Finance</Text>
        </View>
        <View style={styles.navItem}>
          <Ionicons name="bar-chart" size={24} color="#FFFFFF" />
          <Text style={[styles.navLabel, { color: '#FFFFFF' }]}>Analytics</Text>
        </View>
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#121721',
    paddingTop:30
  },
  scrollContainer: {
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
  },
  headerTitle: {
    fontSize: 18,
    fontWeight: '700',
    color: '#FFFFFF',
    textAlign: 'center',
  },
  section: {
    paddingHorizontal: 16,
    marginBottom: 8,
  },
  sectionTitle: {
    fontSize: 18,
    fontWeight: '700',
    color: '#FFFFFF',
    marginBottom: 16,
    paddingVertical: 8,
  },
  metricsGrid: {
    flexDirection: 'row',
    flexWrap: 'wrap',
    gap: 16,
    paddingVertical: 12,
  },
  metricCard: {
    backgroundColor: '#121721',
    borderWidth: 1,
    borderColor: '#334766',
    borderRadius: 8,
    padding: 24,
    minWidth: 158,
    height: 144,
    flex: 1,
    maxWidth: '48%',
  },
  wideCard: {
    maxWidth: '100%',
  },
  metricTitle: {
    fontSize: 16,
    fontWeight: '500',
    color: '#FFFFFF',
    marginBottom: 8,
  },
  metricValue: {
    fontSize: 24,
    fontWeight: '700',
    color: '#FFFFFF',
    marginBottom: 8,
  },
  metricChange: {
    fontSize: 16,
    fontWeight: '500',
    color: '#0AD95E',
  },
  negativeChange: {
    color: '#FA6138',
  },
  positiveChange: {
    color: '#0AD95E',
    fontSize: 16,
    fontWeight: '500',
  },
  trendsContainer: {
    paddingVertical: 12,
  },
  trendsValue: {
    fontSize: 32,
    fontWeight: '700',
    color: '#FFFFFF',
    marginBottom: 8,
  },
  trendsSubInfo: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 4,
    marginBottom: 16,
  },
  trendsSubText: {
    fontSize: 16,
    fontWeight: '400',
    color: '#94A6C7',
  },
  lineChartContainer: {
    height: 200,
    paddingVertical: 16,
  },
  lineChart: {
    height: 148,
    position: 'relative',
    marginBottom: 32,
  },
  chartArea: {
    position: 'absolute',
    width: '100%',
    height: '100%',
    backgroundColor: 'rgba(36, 51, 71, 0.3)',
    borderRadius: 4,
  },
  chartLine: {
    position: 'absolute',
    width: '100%',
    height: 3,
    backgroundColor: '#94A6C7',
    top: '50%',
    zIndex: 1,
  },
  monthLabels: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    paddingHorizontal: 4,
  },
  monthLabel: {
    fontSize: 13,
    fontWeight: '700',
    color: '#94A6C7',
  },
  productsContainer: {
    paddingVertical: 12,
  },
  productsSubInfo: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 4,
    marginBottom: 24,
  },
  productsList: {
    gap: 24,
  },
  productRow: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 16,
  },
  productName: {
    fontSize: 13,
    fontWeight: '700',
    color: '#94A6C7',
    width: 65,
  },
  productBarContainer: {
    flex: 1,
    height: 23.4,
    backgroundColor: '#334766',
    borderRadius: 2,
  },
  productBar: {
    height: '100%',
    backgroundColor: '#243347',
    borderRadius: 2,
    borderRightWidth: 2,
    borderRightColor: '#757575',
  },
  affiliatesContainer: {
    paddingVertical: 12,
  },
  deliveryContainer: {
    paddingVertical: 24,
  },
  chartContainer: {
    height: 220,
  },
  barsContainer: {
    flexDirection: 'row',
    height: 150,
    alignItems: 'flex-end',
    justifyContent: 'space-between',
    marginBottom: 20,
    paddingHorizontal: 4,
  },
  barColumn: {
    flex: 1,
    alignItems: 'center',
    height: '100%',
    justifyContent: 'flex-end',
  },
  bar: {
    width: '60%',
    backgroundColor: '#2563EB',
    borderRadius: 2,
    minHeight: 4,
  },
  labelsContainer: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    paddingHorizontal: 4,
  },
  chartLabel: {
    fontSize: 13,
    fontWeight: '700',
    color: '#94A6C7',
    textAlign: 'center',
    flex: 1,
  },
  bottomNav: {
    flexDirection: 'row',
    backgroundColor: '#192433',
    height: 65,
    paddingTop: 9,
    paddingBottom: 12,
    paddingHorizontal: 16,
    shadowColor: '#000',
    shadowOffset: {
      width: 0,
      height: -3,
    },
    shadowOpacity: 0.25,
    shadowRadius: 6,
    elevation: 10,
  },
  navItem: {
    flex: 1,
    alignItems: 'center',
    justifyContent: 'center',
  },
  navLabel: {
    fontSize: 12,
    fontWeight: '500',
    color: 'rgba(255, 255, 255, 0.3)',
    marginTop: 4,
  },
});

export default AnalyticsDashboard;