import 'package:amsfront/app/routers/app_router.dart';
import 'package:device_preview/device_preview.dart';
import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:amsfront/app/di/injector/injectors.dart';


void main() {
  setupDependencies();
  runApp(DevicePreview(
    enabled: !kReleaseMode,
    isToolbarVisible: false,
    backgroundColor:  const Color.fromARGB(255, 44, 54, 83),
    builder: (context) => const MyApp(),
  ));
}

class MyApp extends StatelessWidget {

  const MyApp({super.key});

  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return MaterialApp.router(
      locale: DevicePreview.locale(context),
      builder: DevicePreview.appBuilder,
      routerConfig: appRouter,
      title: 'Flutter Demo',
      theme: ThemeData(
        colorScheme: ColorScheme.fromSeed(seedColor: Colors.deepPurple),
      ),
    );
  }
}
