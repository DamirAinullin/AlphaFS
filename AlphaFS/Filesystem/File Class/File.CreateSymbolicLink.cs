/*  Copyright (C) 2008-2017 Peter Palotas, Jeffrey Jangli, Alexandr Normuradov
 *  
 *  Permission is hereby granted, free of charge, to any person obtaining a copy 
 *  of this software and associated documentation files (the "Software"), to deal 
 *  in the Software without restriction, including without limitation the rights 
 *  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell 
 *  copies of the Software, and to permit persons to whom the Software is 
 *  furnished to do so, subject to the following conditions:
 *  
 *  The above copyright notice and this permission notice shall be included in 
 *  all copies or substantial portions of the Software.
 *  
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
 *  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN 
 *  THE SOFTWARE. 
 */

using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Security;

namespace Alphaleonis.Win32.Filesystem
{
   partial class File
   {
      /// <summary>[AlphaFS] Creates a symbolic link (soft link).
      /// <remarks>See <see cref="Alphaleonis.Win32.Security.Privilege.CreateSymbolicLink"/> to run this method in an elevated state.</remarks>
      /// </summary>
      /// <exception cref="PlatformNotSupportedException"/>
      /// <exception cref="ArgumentException"/>
      /// <exception cref="ArgumentNullException"/>
      /// <param name="symlinkFileName">The name of the target for the symbolic link to be created.</param>
      /// <param name="targetFileName">The symbolic link to be created.</param>
      /// <param name="targetType">Indicates whether the link target, <paramref name="targetFileName"/>, is a file or directory.</param>      
      [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "symlink")]
      [SecurityCritical]
      public static void CreateSymbolicLink(string symlinkFileName, string targetFileName, SymbolicLinkTarget targetType)
      {
         CreateSymbolicLinkCore(null, symlinkFileName, targetFileName, targetType, PathFormat.RelativePath);
      }


      /// <summary>[AlphaFS] Creates a symbolic link (soft link).
      /// <remarks>See <see cref="Alphaleonis.Win32.Security.Privilege.CreateSymbolicLink"/> to run this method in an elevated state.</remarks>
      /// </summary>
      /// <exception cref="PlatformNotSupportedException"/>
      /// <exception cref="ArgumentException"/>
      /// <exception cref="ArgumentNullException"/>
      /// <param name="symlinkFileName">The name of the target for the symbolic link to be created.</param>
      /// <param name="targetFileName">The symbolic link to be created.</param>
      /// <param name="targetType">Indicates whether the link target, <paramref name="targetFileName"/>, is a file or directory.</param>
      /// <param name="pathFormat">Indicates the format of the path parameter(s).</param>      
      [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "symlink")]
      [SecurityCritical]
      public static void CreateSymbolicLink(string symlinkFileName, string targetFileName, SymbolicLinkTarget targetType, PathFormat pathFormat)
      {
         CreateSymbolicLinkCore(null, symlinkFileName, targetFileName, targetType, pathFormat);
      }


      /// <summary>[AlphaFS] Creates a symbolic link (soft link) as a transacted operation.
      /// <remarks>See <see cref="Alphaleonis.Win32.Security.Privilege.CreateSymbolicLink"/> to run this method in an elevated state.</remarks>
      /// </summary>
      /// <exception cref="PlatformNotSupportedException"/>
      /// <exception cref="ArgumentException"/>
      /// <exception cref="ArgumentNullException"/>
      /// <param name="transaction">The transaction.</param>
      /// <param name="symlinkFileName">The name of the target for the symbolic link to be created.</param>
      /// <param name="targetFileName">The symbolic link to be created.</param>
      /// <param name="targetType">Indicates whether the link target, <paramref name="targetFileName"/>, is a file or directory.</param>      
      [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "symlink")]
      [SecurityCritical]
      public static void CreateSymbolicLinkTransacted(KernelTransaction transaction, string symlinkFileName, string targetFileName, SymbolicLinkTarget targetType)
      {
         CreateSymbolicLinkCore(transaction, symlinkFileName, targetFileName, targetType, PathFormat.RelativePath);
      }


      /// <summary>[AlphaFS] Creates a symbolic link (soft link) as a transacted operation.
      /// <remarks>See <see cref="Alphaleonis.Win32.Security.Privilege.CreateSymbolicLink"/> to run this method in an elevated state.</remarks>
      /// </summary>
      /// <exception cref="PlatformNotSupportedException"/>
      /// <exception cref="ArgumentException"/>
      /// <exception cref="ArgumentNullException"/>
      /// <param name="transaction">The transaction.</param>
      /// <param name="symlinkFileName">The name of the target for the symbolic link to be created.</param>
      /// <param name="targetFileName">The symbolic link to be created.</param>
      /// <param name="targetType">Indicates whether the link target, <paramref name="targetFileName"/>, is a file or directory.</param>
      /// <param name="pathFormat">Indicates the format of the path parameter(s).</param>      
      [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "symlink")]
      [SecurityCritical]
      public static void CreateSymbolicLinkTransacted(KernelTransaction transaction, string symlinkFileName, string targetFileName, SymbolicLinkTarget targetType, PathFormat pathFormat)
      {
         CreateSymbolicLinkCore(transaction, symlinkFileName, targetFileName, targetType, pathFormat);
      }
      



      /// <summary>Creates a symbolic link (soft link).
      /// <remarks>See <see cref="Alphaleonis.Win32.Security.Privilege.CreateSymbolicLink"/> to run this method in an elevated state.</remarks>
      /// </summary>
      /// <exception cref="PlatformNotSupportedException"/>
      /// <exception cref="ArgumentException"/>
      /// <exception cref="ArgumentNullException"/>
      /// <param name="transaction">The transaction.</param>
      /// <param name="symlinkFileName">The name of the target for the symbolic link to be created.</param>
      /// <param name="targetFileName">The symbolic link to be created.</param>
      /// <param name="targetType">Indicates whether the link target, <paramref name="targetFileName"/>, is a file or directory.</param>
      /// <param name="pathFormat">Indicates the format of the path parameter(s).</param>      
      [SecurityCritical]
      internal static void CreateSymbolicLinkCore(KernelTransaction transaction, string symlinkFileName, string targetFileName, SymbolicLinkTarget targetType, PathFormat pathFormat)
      {
         if (!NativeMethods.IsAtLeastWindowsVista)
            throw new PlatformNotSupportedException(new Win32Exception((int) Win32Errors.ERROR_OLD_WIN_VERSION).Message);


         if (pathFormat != PathFormat.LongFullPath)
         {
            const GetFullPathOptions options = GetFullPathOptions.RemoveTrailingDirectorySeparator | GetFullPathOptions.FullCheck;

            symlinkFileName = Path.GetExtendedLengthPathCore(transaction, symlinkFileName, pathFormat, options);
            targetFileName = Path.GetExtendedLengthPathCore(transaction, targetFileName, pathFormat, options);
         }


         // Don't use long path notation, as it will be empty upon creation.
         targetFileName = Path.GetRegularPathCore(targetFileName, GetFullPathOptions.None, false);


         var success = null == transaction

            // CreateSymbolicLink() / CreateSymbolicLinkTransacted()
            // In the ANSI version of this function, the name is limited to MAX_PATH characters.
            // To extend this limit to 32,767 wide characters, call the Unicode version of the function and prepend "\\?\" to the path.
            // 2014-02-14: MSDN does not confirm LongPath usage but a Unicode version of this function exists.
            // 2015-07-17: This function does not support long paths.
            // 2017-05-30: MSDN confirms LongPath usage: Starting with Windows 10, version 1607

            ? NativeMethods.CreateSymbolicLink(symlinkFileName, targetFileName, targetType)
            : NativeMethods.CreateSymbolicLinkTransacted(symlinkFileName, targetFileName, targetType, transaction.SafeHandle);

         var lastError = Marshal.GetLastWin32Error();
         if (!success)
            NativeError.ThrowException(lastError, symlinkFileName, targetFileName);
      }
   }
}
