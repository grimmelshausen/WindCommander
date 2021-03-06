function [DriveResult,HeelResult] = sailCalc(AWS, AWA, output)

% from http://www.wb-sails.fi/Portals/209338/news/SailPowerCalc/SailPowerCalc.htm

P = 10; %main luff
Emeas = 2.6; %main foo
I= 7.2; %jib hoist
J= 1.7; %jib base
spinArea = 0; %spinnaker area

LP = 150; %percent J

BAS= 0.8; %boom height
%AWS= 9; %apparent wind speed
%AWA= 25; %apparent wind angle
HEEL= 0 ;
RMC= 78;

%***Calculate sail areas

mainArea =round(10*0.59*P*Emeas)/10;
jibArea =round(10*0.5*LP/100*I*J)/10;
% spinArea =round(10*0.9*1.8*J*sqrt(I*I+J*J))/10;
spinArea = round(10*spinArea)/10;

mainAreaft =round(mainArea/0.093);
jibAreaft =round(jibArea/0.093);
spinAreaft =round(spinArea/0.093);

%***Output sail areas
%input.mainResult.value=mainArea;
%input.jibResult.value=jibArea;
%input.spinResult.value=spinArea;

%input.mainftResult.value=mainAreaft;
%input.jibftResult.value=jibAreaft;
%input.spinftResult.value=spinAreaft;

%***Rig measurements in feet
%Pfeet= round(10*P/0.3048)/10;
%Efeet= round(10*Emeas/0.3048)/10;
%Ifeet= round(10*I/0.3048)/10;
%Jfeet= round(10*J/0.3048)/10;
%spinAreafeet= round(10*spinArea/0.0929)/10;
%BASfeet=  round(10*BAS/0.3048)/10;

%AWSknots=  round(1*AWS/0.5144)/1;
%input.AWSknotsResult.value=AWSknots;


%input.PfeetResult.value=Pfeet;
%input.EfeetResult.value=Efeet;
%input.IfeetResult.value=Ifeet;
%input.JfeetResult.value=Jfeet;
%input.spinAreafeetResult.value=spinAreafeet;
%input.BASfeetResult.value=BASfeet;




%***Calculate sail forces
genLuff= 0.95*sqrt(I*I+J*J);

genLuffFactr= 0.965;

if(LP>100)
    genLPfactr= 1.3 - 0.003*LP;
else
    genLPfactr= 1;
end

effUpwindArea= mainArea + genLPfactr*jibArea;
% effDownwindArea= mainArea + 0.75*spinArea;
if(0.75*spinArea < genLPfactr*jibArea)
    effDownwindArea= mainArea + genLPfactr*jibArea + 0.75*spinArea     ;
else
    effDownwindArea= mainArea + 0.75*spinArea;
end

depowringEnabled  = 0;
if(AWS < 7)
    dePowrFctr= 1;
elseif (AWS < 11 && depowringEnabled)
    dePowrFctr= 1- mainArea/effUpwindArea*(1-(1.7-0.1*AWS))	;
elseif (AWS < 21 && depowringEnabled)
    dePowrFctr= 1- mainArea/effUpwindArea*(1-(0.93-0.03*AWS));
else
    dePowrFctr= 1;
end

% only main can be depowered

%input.dePowrFctrResult.value=round(1000*dePowrFctr)/1000;

%***Rig aspect ratio
if (P+BAS > I)
    rigAr= ((P+BAS)^2)/effUpwindArea ;
else
    rigAr= (I^2)/effUpwindArea;
end

%input.arResult.value=round(10*rigAr)/10;

ArDFCcorr =0.00054*((rigAr+0.33)^3)-0.01212*((rigAr+0.33)^2)+0.108*(rigAr+0.33)-0.2595;
ArHFCcorr=0.00243*((rigAr+0.33)^3)-0.0456*((rigAr+0.33)^2)+0.32*(rigAr+0.33)-0.6775;

%input.arDFCcorrResult.value=round(1000*ArDFCcorr)/1000;
%input.ArHFCcorrResult.value=round(1000*ArHFCcorr)/1000;

heightCE= ((dePowrFctr^0.5)*mainArea*0.39*(P+BAS)+jibArea*0.37*I)/effUpwindArea;

%input.heightCEResult.value=round(100*heightCE/(P+BAS));

DFC20= 0.32+ArDFCcorr;
DFC40= 0.43;
DFCHeel= DFC20*cos(pi/180*HEEL)*cos(pi/180*HEEL);

% input.DFC20Result.value=round(1000*DFC20)/1000;
% input.DFCHeelResult.value=round(1000*DFCHeel)/1000;

HFC20=1.17+ArHFCcorr;

spinSetAWA= -0.048*((0.9*AWS)^3)+1.54*((0.9*AWS)^2)-9.1*(0.9*AWS)+60 ;
% alunperin spinSetAWAn parametrina TWS

% DFCnospi=dePowrFctr*(-9.535E-04*pow(AWA,2)+0.07105*AWA-.8+ArDFCcorr); Brummer
DFCnospi=dePowrFctr*(0.26E-06*(AWA^3)-1.7E-04*(AWA^2)+0.034*(AWA)-0.4+ArDFCcorr);

%alunperin -0.728 _ vaikuttaa suoraan DFC:n arvoon

%input.spinSetAWAResult.value=round(spinSetAWA);

% DFCspi= dePowrFctr*(-8.5E-05*pow(AWA,2)+0.02085*AWA-0.28707);
DFCspi= dePowrFctr*(-1.55E-04*(AWA^2)+0.0333*AWA-0.33);

if(AWA < spinSetAWA)
    corrDFC= (1-(1-DFCHeel/DFC20)/30*HEEL)*DFCnospi;
else
    corrDFC= (1-(1-DFCHeel/DFC20)/30*HEEL)*DFCspi;
end

%input.corrDFCResult.value=round(1000*corrDFC)/1000;

if(AWA < spinSetAWA)
    if (output)
        disp('No Spinnaker')
    end
else
   if (output)
       disp('Spinnaker UP')
   end
end


if(AWA < 20)
    corrHFC= (dePowrFctr^1.5)*(8.838E-07*(((AWA-0)*cos(pi/180*HEEL))^3) -  0.000363*(((AWA-0)*cos(pi/180*HEEL))^2) + 0.033146*((AWA-0)*cos(pi/180*HEEL)) + 0.63);
else
    corrHFC= (dePowrFctr^1.5)*(-2.2E-08*(((AWA+0)*cos(pi/180*HEEL))^4)+10.35E-06*(((AWA+0)*cos(pi/180*HEEL))^3) -1.65E-3*(((AWA+0)*cos(pi/180*HEEL))^2)+ 0.0888*((AWA+0)*cos(pi/180*HEEL)) + 0.1);
end

%alunperin + 0.63 else + 0.594 _ vaikuttaa suoraan HFC:n arvoon

%input.corrHFCResult.value=round(1000*corrHFC)/1000;
if (output)
    disp(['corrHFCResult=' num2str(round(1000*corrHFC)/1000)])
end

liftCL= sin(pi/180*AWA)*corrDFC+cos(pi/180*AWA)*corrHFC;
dragCD= sin(pi/180*AWA)*corrHFC-cos(pi/180*AWA)*corrDFC;

%input.liftCLResult.value=round(1000*liftCL)/1000;
if (output)
    disp(['liftCLResult=' num2str(round(1000*liftCL)/1000)]);
end
%input.dragCDResult.value=round(1000*dragCD)/1000;
if (output)
    disp(['dragCDResult=' num2str(round(1000*dragCD)/1000)]);
end


sailSideForce= corrHFC*0.5*1.25*AWS*AWS*effUpwindArea/9.81*cos(pi/180*HEEL);

if(AWA < 20)
    leeWay= 20 - 0.8*AWA;
else
    leeWay= 4.5 - 0.025*AWA;
end

%input.leeWayResult.value=round(10*leeWay)/10;
if (output) 
    disp(['leeWayResult=' num2str(round(1000*leeWay)/1000)]);
end


if(AWA < spinSetAWA)
    sailDrive=corrDFC*0.5*1.25*AWS*AWS*effUpwindArea/9.81*cos(pi/180*sin(pi/180*HEEL)*leeWay)+sin(pi/180*leeWay)*(sailSideForce);
else
    sailDrive=corrDFC*0.5*1.25*AWS*AWS*effDownwindArea/9.81*cos(pi/180*sin(pi/180*HEEL)*leeWay)+sin(pi/180*leeWay)*(sailSideForce);
end

heelMom= 1.35*heightCE*sailSideForce;

DriveResult = (sailDrive);
if (output) 
    disp(['DriveResult=' num2str(round(1000*sailDrive)/1000)]);
end
HeelResult = sailSideForce;
if (output)
    disp(['HeelResult=' num2str(round(1000*sailSideForce)/1000)]);
end
%input.heelMomResult.value= round(heelMom);
if (output)
    disp(['heelMomResult=' num2str(round(1000*heelMom)/1000)]);
end


%Convert to lbs
%sailDrivelb= round(sailDrive/0.454);
%sailSideForcelb= round(sailSideForce/0.454);
%heelMomlb= round(heightCE/0.3048*sailSideForcelb);
%RMClb=round(RMC/0.454/0.3048);

%input.DrivelbResult.value=sailDrivelb;
%input.HeellbResult.value=sailSideForcelb;
%input.heelMomlbResult.value=heelMomlb;
%input.RMClbResult.value=RMClb;

%driveHMratio=  round(1000*sailDrive/heelMom)/1000;
%input.driveHMratioResult.value= driveHMratio;

estHeel=round(heelMom/RMC/cos(pi/180*HEEL));
%input.estHeelResult.value= estHeel;
if (output)
    disp(['estHeelResult=' num2str(estHeel)]); 
end