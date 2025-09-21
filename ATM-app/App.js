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

export default function App() {

const Stack = createStackNavigator();  
  return (
<NavigationContainer>
<Stack.Navigator initialRouteName='addproduct'>
<Stack.Screen name='signin' component={SignIn} options={{headerShown:false}}/>
<Stack.Screen name='signup' component={SignUp} options={{headerShown:false}}/>
<Stack.Screen name='admindashboard' component={AdminDashboard} options={{headerShown:false}}/>
<Stack.Screen name='financials' component={FinancialsScreen} options={{headerShown:false}}/>
<Stack.Screen name='products' component={ProductsScreen} options={{headerShown:false}}/>
<Stack.Screen name='affiliate' component={AffiliatesScreen} options={{headerShown:false}}/>
<Stack.Screen name='analytics' component={AnalyticsDashboard} options={{headerShown:false}}/>
<Stack.Screen name='addproduct' component={AddProductScreen} options={{headerShown:false}}/>

</Stack.Navigator>


</NavigationContainer>


  );
}


