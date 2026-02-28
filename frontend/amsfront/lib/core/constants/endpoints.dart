

import 'package:graphql_flutter/graphql_flutter.dart';

class Endpoints {
// Use 10.0.2.2 for Android Emulator to connect
static const devBaseUrl = "https://localhost:7170";
static const iosBaseUrl = "https://localhost:7170"; // iOS simulator
static const prodBaseUrl ="https://api.myapp.com"; // deployed server
static const login = "/api/Auth/login";
static const register = "/api/register";

 static  HttpLink  dashboardhttpLink = HttpLink('https://localhost:7170/graphql');

}