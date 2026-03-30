
import 'package:amsfront/app/enums/roles.dart';
import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';
  
class switcher 
{
 

static void routing(BuildContext context,roles _role)
{
switch(_role)
{
  case roles.None:
  break;
  case roles.SuperAdmin:
context.push('/SuperAdmin');
break;
  case roles.Assistant:
  context.push('/Assisstant');
  break;
  case roles.Affiliate:
  context.push('/Affiliate');
  break;
  case roles.Driver:
  context.push('/Driver');
  break;

  case roles.Admin:

    throw UnimplementedError();
}
}

  static void roleRouting(BuildContext context, String? extraInfoRole,Object extra) {
    if (extraInfoRole == null) return;
    switch (extraInfoRole) {
      case 'Admin':
        context.push('/Admin-info', extra: extra);
        break;
      case 'Driver':
        context.push('/Driver-info', extra: extra);
        break;
      case 'Affiliate':
        context.push('/affiliate-info', extra: extra);
        break;
      case 'Assisstant':
        context.push('/Assisstant-info', extra: extra);
        break;
    }
  }
}

