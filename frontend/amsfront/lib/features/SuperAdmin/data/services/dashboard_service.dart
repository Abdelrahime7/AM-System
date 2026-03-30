import 'package:amsfront/core/constants/endpoints.dart';
import 'package:amsfront/core/network/api_clients.dart';
import 'package:amsfront/features/SuperAdmin/data/model/dashboardModel.dart';
import 'package:amsfront/features/SuperAdmin/data/model/status_request.dart';
import 'package:amsfront/features/SuperAdmin/data/model/user_model.dart';
import 'package:graphql_flutter/graphql_flutter.dart';






const String dashboardQuery = r'''
  query {
    dashboard {
    totalSales
    activeAffiliates
    pendingOrders 
    totalRevenue 
    userRecent 
    financRecent 
    productRecent
    }
  }
''';


class DashboardService {
  final GraphQLClient client;
  final ApiClient apiClient;


  DashboardService(this.client,this.apiClient);


  Future<DashboardModel> getDashboardData() async {
    final QueryOptions options = QueryOptions(
      document: gql(dashboardQuery),
    );


    final result = await client.query(options);

    if (result.hasException) {
      print(result.exception.toString());
      throw Exception(result.exception.toString());
    }

    return DashboardModel.fromJson(result.data?['dashboard']);
  }

 Future<List<UserResponse>> getUsers() async {
  final response = await apiClient.get(Endpoints.users);

  if (response.statusCode == 200) {
    final List<dynamic> data = response.data;

    return data
        .where((json) => json != null) // filter out nulls just in case
        .map((json) => UserResponse.fromJson(json as Map<String, dynamic>))
        .toList();
  } else {
    throw Exception("Failed to load users: ${response.statusCode}");
  }
}

Future <void> changeUserStatus(StatusRequest request)
async {
  final response= await apiClient.patch(Endpoints.changeUserStatus, data: request.toJson());
  if (response.statusCode !=200 )
  {
     throw Exception("Failed to change user status : ${response.statusCode}");
  }
  

  
}

}
