import 'package:flutter/material.dart';

class AboutUsScreen extends StatelessWidget {
  const AboutUsScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        backgroundColor: const Color.fromARGB(255, 44, 54, 83),
        elevation: 0,
        iconTheme: const IconThemeData(color: Colors.white),
        title: const Text(
          'About Us | من نحن',
          style: TextStyle(color: Colors.white),
        ),
        centerTitle: true,
      ),
      body: Container(
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
        child: SingleChildScrollView(
          padding: const EdgeInsets.all(24.0),
          child: Column(
            children: [
              const Text(
                'BMG CORP',
                style: TextStyle(
                  color: Color(0xFF2563EB),
                  fontSize: 28,
                  fontWeight: FontWeight.bold,
                ),
              ),
              const SizedBox(height: 24),
              Text(
                'BMG CORP هو تطبيق مخصّص لتسيير وتنظيم التسويق بالعمولة، صُمّم خصيصًا لتمكين المسوّقين من العمل بثقة ووضوح، ولمنح التجّار تجربة إدارة أكثر سلاسة واحترافية.\n\n'
                'أنشأنا هذا التطبيق انطلاقًا من ملاحظة تحدٍّ متكرر في مجال التسويق بالعمولة:\n'
                'غياب الشفافية، تعقيد الحسابات، وكثرة الخلافات حول العمولات.\n\n'
                'لهذا جاء BMG CORP ليكون الحل.\n\n'
                'يعمل التطبيق على حساب العمولات تلقائيًا وبشكل دقيق لكل مسوّق على حدة، دون تدخل يدوي أو مجال للخطأ، مما يضمن العدالة، الوضوح، وبناء الثقة بين جميع الأطراف.\n\n'
                'نحن نؤمن أن المسوّق الناجح يحتاج إلى نظام يعتمد عليه، لا إلى وعود.\n\n'
                'ولهذا ركّزنا على تبسيط تجربة التسويق بالعمولة، وتمكين المسوّقين من التركيز على ما يجيدونه: التسويق والبيع، بينما يتكفّل التطبيق بكل ما يتعلق بالحسابات والتنظيم.\n\n'
                'BMG CORP ليس مجرد أداة، بل شريك يرافقك في رحلتك نحو تسويق أكثر احترافية واستقرارًا.',
                textAlign: TextAlign.center,
                style: const TextStyle(
                  color: Colors.white,
                  fontSize: 16,
                  height: 1.8,
                ),
                textDirection: TextDirection.rtl,
              ),
            ],
          ),
        ),
      ),
    );
  }
}
