
import 'package:amsfront/app/enums/roles.dart';
import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';

class switcher 
{
final roles _role;

const switcher(this._role);

void  routing(BuildContext context)
{
switch(_role)
{
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
  
   


}

}

}