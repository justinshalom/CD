﻿' Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

' vbc /t:library /vbruntime- A.vb

Imports System.Collections.Generic
Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices

<Assembly: Guid("f9c2d51d-4f44-45f0-9eda-c9d599b5826A")> 
<Assembly: ImportedFromTypeLib("A.dll")> 

<ComImport(), Guid("27e3e649-994b-4f58-b3c6-f8089a5f200A"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)> _
Public Interface IA
End Interface
