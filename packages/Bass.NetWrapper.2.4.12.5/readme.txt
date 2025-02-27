BASS.NET API for the Un4seen BASS Audio Library
-----------------------------------------------
Requires  : BASS Audio Library plus add-ons - available @ www.un4seen.com
            Works with the Microsoft .NET Framework v2.0, v3.0, v3.5, v4.0 and v4.5 and supports Visual Studio 2008, 2010, 2012 or above.
Copyright : (c) 2005-2016 by radio42, Hamburg, Germany
Author    : Bernd Niedergesaess, bn@radio42.com

Purpose   : .NET API wrapper for the Un4seen BASS Audio libraray


WARNING
-------
TO THE MAXIMUM EXTENT PERMITTED BY APPLICABLE LAW, BASS.NET IS PROVIDED
"AS IS", WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED,
INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY
AND/OR FITNESS FOR A PARTICULAR PURPOSE. THE AUTHORS SHALL NOT BE HELD
LIABLE FOR ANY DAMAGE THAT MAY RESULT FROM THE USE OF BASS.NET. BASICALLY,
YOU USE BASS.NET ENTIRELY AT YOUR OWN RISK.


SETUP
-----
Unzip the content to a new and empty folder.
NOTE:
There are several Bass.Net versions deployed (each in a seperate sub-folder):
.\v2.0    : Bass.Net for .Net 2.0 (or 3.0, 3.5)
.\v4.5    : Bass.Net for .Net 4.5 (or 4.6)
.\compact : Bass.Net CE for .Net CompactFramework 3.5
.\iOS     : Bass.Net iOS for Xamarin.iOS
.\Android : Bass.Net Android for Mono.Android
.\Linux   : Bass.Net Linux for .Net 4.5
.\OSX     : Bass.Net MAC OSX for .Net 4.5
When writing your own application using BASS.NET you can simple add a new project reference and select the 
appropriate "BASS.NET API" from the standard .NET components tab targeting your used .Net Framework version - that's all!
The native BASS libraries are NOT included here and need to be downloaded seperately.
In order to run the samples provided, you need to copy the relevant BASS libraries to the output directories first!


REGISTRATION
------------
You can obtain your License and Registration-Key here: http://www.bass.radio42.com

After you have received your Registration-Key, you might disable the splash screen by calling the following method prior to any other BASS method:
BassNet.Registration("<your email>", "<your regkey>");


LICENSE
-------
PLease see below under 'Cost'!


INCLUDED/INSTALLED FILES
------------------------
\<install-dir>:
  LICENSE.rtf            : the BASS.NET API license file
  readme.txt             : setup and general information text file (this)
  .\v2.0
    Bass.Net.dll         : the BASS.NET API library (targeting the .Net Framework 2.0)
    Bass.Net.xml         : the BASS.NET API xml documentation file (needed for IntelliSense)
  .\v4.5
    Bass.Net.dll         : the BASS.NET API library (targeting the .Net Framework 4.5)
    Bass.Net.xml         : the BASS.NET API xml documentation file (needed for IntelliSense)
  .\compact
    Bass.Net.compact.dll : the BASS.NET CE API library (targeting the .Net CompactFramework 3.5)
    Bass.Net.compact.xml : the BASS.NET CE API xml documentation file (needed for IntelliSense)
  .\iOS
    Bass.Net.iOS.dll     : the BASS.NET iPhone API library (targeting Xamarin.iOS, static '__Internal')
    Bass.Net.iOS.xml     : the BASS.NET iPhone API xml documentation file (needed for IntelliSense)
  .\OSX
    Bass.Net.OSX.dll     : the BASS.NET OSX API library (targeting the .Net Framework 4.5)
    Bass.Net.OSX.xml     : the BASS.NET OSX API xml documentation file (needed for IntelliSense)
  .\Linux
    Bass.Net.Linux.dll     : the BASS.NET Linux API library (targeting the .Net Framework 4.5)
    Bass.Net.Linux.xml     : the BASS.NET Linux API xml documentation file (needed for IntelliSense)
  .\Help
    Bass.Net.chm         : the actual MS Help 1.x offline help file for the BASS.NET API
  .\Samples:
    \CS                   : this folder contains all C# examples and a global solution for it (sample.sln)
    \VB                   : this folder contains all VB examples and a global solution for it (sample.sln)


What's the point?
-----------------
BASS.NET is an API implementation to be used with the Microsoft .NET Framework. The API can be used with any .NET language, 
like C#, Visual Basic, JScript or managed C++. It is based on managed C# code.
MS Help 2.x files are provided and integrate into VS to support online help for all BASS functions.


Requirements
------------
BASS 2.4 is required (bass.dll). See www.un4seen.com for details!
In addition the respective add-ons are required as well, if you call any method from the "Un4seen.Bass.AddOn" namespace.


Copyright
---------
BASS.NET API: Copyright 2005-2016 by radio42, Author: Bernd Niedergesaess  (bn@radio42.com). All rights reserved. 
BASS.NET is the property of radio42 and is protected by copyright laws and international copyright treaties. BASS.NET is not sold, it is licensed.
BASS and Add-Ons: All trademarks and other registered names contained in the BASS.NET package are the property of their respective owners.
See www.un4seen.com for details!


Disclaimer
----------
The freeware version of BASS.NET is free for non-money making use. 
If you are not charging for or making any money with your software AND you are an individual person (not a company) AND 
your software is intended for personal use only, then you can use the BASS.NET API in your software for free.
Free in this case means, that the you can use BASS.NET without any further license fees.
It does NOT mean that you are free to change, copy, redistribute, share or use BASS.NET in any purpose.

If you wish to use BASS.NET in shareware or commercial products (or it has other commercial purpose, e.g. advertising, training etc.), 
you will require a separate license from radio42 (see the Cost section for details).

By using this software, you agree to the following conditions:
1) The freeware version of BASS.NET API is distributed under the license of radio42 (see LICENSE.rtf).
2) The LICENSEE may not charge for or make any money with the software using the freeware version of BASS.NET.
3) It is prohibited to change any of the provided source code and ALL the files must at any time remain intact and unmodified.
4) You may not decompile, disassemble, reverse engineer or modify any part of BASS.NET.
5) The LICENSEE may ONLY distribute the DLL part of BASS.NET with your software (Bass.Net.dll), no other part of BASS.NET may be distributed.
6) You may not resell or sublicense BASS.NET.
7) A splash screen will appear every time at start-up (unless you obtained a valid Registration-Key).
8) You are NOT allowed to pass your personal Registration-Key to anyone at anytime!
Please note, that you also need to take care of all BASS modules and their respective rights.


Cost
----
BASS.NET is free for non-commercial use. If you are a non-commercial entity (eg. an individual) and you are not charging for your product, 
and the product has no other commercial purpose, then you can use BASS.NET in it for free.
Otherwise, you will require one of the following licences:

Shareware license: 29.00 Euro 
(The "shareware" licence allows the usage of BASS.NET in an unlimited number of your shareware products, which must sell for no more than 40 Euros each.
If you're an individual (not a corporation) making and selling your own software (and its price is within the limit), this is the licence for you.)

Single Commercial license: 199.00 Euro
(The "single commercial" license allows the usage of BASS.NET API in a single commercial product)

Unlimited Commercial license: 499.00 Euro 
(The "unlimited commercial" license allows the usage of BASS.NET API in an unlimited number of your commercial products. 
This license applies to a single site)

Please note the products must be end-user products, e.g. not components used by other products. 
These licences only cover your own software. Not the publishing of other's software. 
If you publish other's software, its developers (or the software itself) will need to be licensed to use BASS.NET.

In all cases there are no royalties to pay, and you can use all future updates without further cost. 
Reselling is not permitted.

You can obtain a shareware or commercial license here: http://www.bass.radio42.com
Contact:
radio42, Bernd Niedergesaess
Gryphiusstrasse 9
22299 Hamburg, Germany
Mail: bn@radio42.com 


Registration
------------
You can obtain your License and Registration-Key here: http://www.bass.radio42.com

After you have received your Registration-Key, you might disable the splash screen by calling the following method prior to any other BASS method:
BassNet.Registration("<your email>", "<your regkey>");


Third party intellectual property rights
----------------------------------------
BASS.NET is a .Net wrapper of the product BASS (www.un4seen.com). In order to use BASS.NET an additional BASS license needs to be obtained separately.
MP3 technology is patented, and so the use of the BASS MP3 decoder in the PRODUCTS requires the LICENSEE to have a patent license from Thomson (www.mp3licensing.com).
Alternatively, the LICENSEE does not need a patent license if BASS is set to use the already licensed Windows MP3 decoder.
