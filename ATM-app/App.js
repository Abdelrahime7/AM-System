import { NavigationContainer, useNavigation } from '@react-navigation/native';
import { createStackNavigator } from '@react-navigation/stack';
import { StatusBar } from 'expo-status-bar';
import { StyleSheet, Text, View } from 'react-native';
import SignIn from './Components/Auth/SignIn';
import SignUp from './Components/Auth/SignUp';
import AdminDashboard from './Components/AdminScreens/AdminDashboard';
import FinancialsScreen from './Components/AdminScreens/FinancialsScreen';
import ProductsScreen from './Components/AdminScreens/ProductsScreen';
import AffiliatesScreen from './Components/AdminScreens/AffiliatesScreen';
import AnalyticsDashboard from './Components/AdminScreens/AnalyticsDashboard';
import AddProductScreen from './Components/AdminScreens/AddProductScreen';
import AffiliateHomeScreen from './Components/AffiliateScreens/AffiliateHomeScreen';
import AffiliateProductsScreen from './Components/AffiliateScreens/AffiliateProductsScreen';
import WithdrawalScreen from './Components/AffiliateScreens/WithdrawalScreen';
import ProfileScreen from './Components/AffiliateScreens/ProfileScreen';
import CreateOrderScreen from './Components/AffiliateScreens/CreateOrderScreen';
import ProductDetailsScreen from './Components/AffiliateScreens/ProductDetailsScreen';
import ProductCreationForm from './Components/AffiliateScreens/ProductCreationForm';
import SuccessScreen from './Components/AffiliateScreens/SuccessScreen';
<<<<<<< HEAD
import AssignedOrdersScreen from './Components/LocalDriversScreens/AssignedOrdersScreen';
import UpdateDeliveryScreen from './Components/LocalDriversScreens/UpdateDeliveryScreen';
import AdminAssistantDashboard from './Components/AdminAssistantScreens/AdminAssistantDashboard';
import OrderQueueScreen from './Components/AdminAssistantScreens/OrderQueueScreen';
import SplashScreen from './Components/SplashScreen/SplashScreen';
=======
>>>>>>> ec74cefecfa930d7e93b8c8e44ac1db42397955c

export default function App() {

const Stack = createStackNavigator();  
  return (
<NavigationContainer>
<<<<<<< HEAD
<Stack.Navigator initialRouteName='splash'>
=======
<Stack.Navigator initialRouteName='success'>
>>>>>>> ec74cefecfa930d7e93b8c8e44ac1db42397955c
<Stack.Screen name='signin' component={SignIn} options={{headerShown:false}}/>
<Stack.Screen name='signup' component={SignUp} options={{headerShown:false}}/>
<Stack.Screen name='admindashboard' component={AdminDashboard} options={{headerShown:false}}/>
<Stack.Screen name='financials' component={FinancialsScreen} options={{headerShown:false}}/>
<Stack.Screen name='products' component={ProductsScreen} options={{headerShown:false}}/>
<Stack.Screen name='affiliate' component={AffiliatesScreen} options={{headerShown:false}}/>
<Stack.Screen name='analytics' component={AnalyticsDashboard} options={{headerShown:false}}/>
<Stack.Screen name='addproduct' component={AddProductScreen} options={{headerShown:false}}/>
<Stack.Screen name='affiliatehome' component={AffiliateHomeScreen} options={{headerShown:false}}/>
<Stack.Screen name='productsaffiliate' component={AffiliateProductsScreen} options={{headerShown:false}}/>
<Stack.Screen name='withdrawal' component={WithdrawalScreen} options={{headerShown:false}}/>
<Stack.Screen name='profile' component={ProfileScreen} options={{headerShown:false}}/>
<Stack.Screen name='createorder' component={CreateOrderScreen} options={{headerShown:false}}/>
<Stack.Screen name='productdetails' component={ProductDetailsScreen} options={{headerShown:false}}/>
<Stack.Screen name='productcreationform' component={ProductCreationForm} options={{headerShown:false}}/>
<Stack.Screen name='success' component={SuccessScreen} options={{headerShown:false}}/>
<<<<<<< HEAD
<Stack.Screen name='assignedorders' component={AssignedOrdersScreen} options={{headerShown:false}}/>
<Stack.Screen name='adminassistantdashboard' component={AdminAssistantDashboard} options={{headerShown:false}}/>
<Stack.Screen name='orderqueue' component={OrderQueueScreen} options={{headerShown:false}}/>
<Stack.Screen name='splash' component={SplashScreen} options={{headerShown:false}}/>

=======
>>>>>>> ec74cefecfa930d7e93b8c8e44ac1db42397955c

</Stack.Navigator>


</NavigationContainer>


  );
}


