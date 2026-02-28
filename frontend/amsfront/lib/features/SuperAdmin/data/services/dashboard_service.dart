import 'package:amsfront/features/SuperAdmin/data/model/dashboardModel.dart';
import 'package:graphql_flutter/graphql_flutter.dart';


class DashboardService {
  final GraphQLClient client;

  DashboardService(this.client);

  Future<DashboardModel> getDashboardData() async {
    final QueryOptions options = QueryOptions(
      document: gql(dashboardQuery),
    );

    final QueryResult result = await client.query(options);

    if (result.hasException) {
      throw Exception(result.exception.toString());
    }
return DashboardModel.fromJson(result.data?['dashboard']);

  }
}



const String dashboardQuery = r'''
  query {
    dashboard {
      totalSales
      activeAffiliates
      pendingOrders
      totalRevenue
    }
  }
''';
