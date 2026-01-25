import 'package:flutter/material.dart';

class OurWorksScreen extends StatefulWidget {
  const OurWorksScreen({super.key});

  @override
  State<OurWorksScreen> createState() => _OurWorksScreenState();
}

class _OurWorksScreenState extends State<OurWorksScreen> {
  late PageController _pageController;
  int _currentPage = 0;

  // List of image assets. Ensure these exist in your assets/images directory
  // and are defined in your pubspec.yaml file.
  final List<String> _worksImages = [
    
    'assets/images/photo1.jpg',
    'assets/images/photo2.jpg',
    'assets/images/photo3.jpg',
    
    
  ];

  @override
  void initState() {
    super.initState();
    // viewportFraction < 1.0 allows the side images to be visible.
    _pageController = PageController(initialPage: _currentPage, viewportFraction: 0.7);
  }

  @override
  void dispose() {
    _pageController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        backgroundColor: const Color.fromARGB(255, 44, 54, 83),
        iconTheme: const IconThemeData(color: Colors.white),
        title: const Text(
          'Our Works',
          style: TextStyle(color: Colors.white),
        ),
        centerTitle: true,
      ),
      body: Container(
        width: double.infinity,
        height: double.infinity,
        decoration: const BoxDecoration(
          gradient: LinearGradient(
            begin: Alignment.topCenter,
            end: Alignment.bottomCenter,
            colors: [
              Color(0xFF1A1F2E),
              Color(0xFF0F131C),
            ],
          ),
        ),
        child: Center(
          child: SizedBox(
            height: 450, // Adjust the height of the carousel area
            child: PageView.builder(
              controller: _pageController,
              itemCount: _worksImages.length,
              physics: const BouncingScrollPhysics(),
              onPageChanged: (int index) {
                setState(() => _currentPage = index);
              },
              itemBuilder: (context, index) => _buildCarouselItem(index),
            ),
          ),
        ),
      ),
    );
  }

  Widget _buildCarouselItem(int index) {
    return AnimatedBuilder(
      animation: _pageController,
      builder: (context, child) {
        double value = 0.0;
        if (_pageController.position.haveDimensions) {
          value = index.toDouble() - (_pageController.page ?? 0);
        } else {
          value = (index - _currentPage).toDouble();
        }

        // Calculate the scale based on the distance from the center.
        // The item at the center (value 0) will have scale 1.0.
        // Items further away will be scaled down.
        double scale = (1 - (value.abs() * 0.3)).clamp(0.0, 1.0);
        scale = Curves.easeOut.transform(scale);

        return Transform.scale(
          scale: scale,
          child: child,
        );
      },
      child: Container(
        margin: const EdgeInsets.all(10),
        decoration: BoxDecoration(
          borderRadius: BorderRadius.circular(20),
          boxShadow: [
            BoxShadow(
              color: Colors.black.withOpacity(0.2),
              blurRadius: 10,
              offset: const Offset(0, 5),
            ),
          ],
          image: DecorationImage(
            image: AssetImage(_worksImages[index]),
            fit: BoxFit.cover,
          ),
        ),
      ),
    );
  }
}
