﻿#region License
//
// Linux.cs
//
// Author:
//       Stefanos A. <stapostol@gmail.com>
//       Jean-Philippe Bruyère <jp.bruyere@hotmail.com>
//
// Copyright (c) 2006-2014 Stefanos Apostolopoulos
// Copyright (c) 2013-2017 Jean-Philippe Bruyère
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
#endregion

using System;
using System.Runtime.InteropServices;
using System.Text;

#pragma warning disable 0649 // field is never assigned

namespace Linux
{
    partial class Libc
    {
        const string lib = "libc";

        [DllImport(lib)]
        public static extern int dup(int file);

        [DllImport(lib)]
        public static extern int dup2(int file1, int file2);

        [DllImport(lib)]
        public static extern int ioctl(int d, uint request, [Out] IntPtr data);

        [DllImport(lib)]
        public static extern int open([MarshalAs(UnmanagedType.LPStr)]string pathname, OpenFlags flags);

        [DllImport(lib)]
        public static extern int open(IntPtr pathname, OpenFlags flags);

		[DllImport(lib)]
		public static extern int posix_openpt (OpenFlags flags);

        [DllImport(lib)]
        public static extern int close(int fd);

        [DllImport(lib)]
        unsafe public static extern IntPtr read(int fd, void* buffer, UIntPtr count);
		[DllImport(lib)]
		unsafe public static extern uint write(int fd, void *buffer, int count); 

		public static void write (int fd, byte[] b){
			unsafe {
				fixed (byte* pb = b) {
					write (fd, pb, b.Length); 
				}
			}
		}

        public static int read(int fd, out byte b)
        {
            unsafe
            {
                fixed (byte* pb = &b)
                {
                    return read(fd, pb, (UIntPtr)1).ToInt32();
                }
            }
        }

        public static int read(int fd, out short s)
        {
            unsafe
            {
                fixed (short* ps = &s)
                {
                    return read(fd, ps, (UIntPtr)2).ToInt32();
                }
            }
        }

        [DllImport(lib)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool isatty(int fd);

		[DllImport(lib)]
		public static extern int getgid ();
		[DllImport(lib)]
		public static extern int setpgid (int pid, int pgid);
		[DllImport(lib)]
		public static extern int getpgid (int pid);

		//public static extern int setpgrp (int pgid);
		[DllImport(lib)]
		public static extern int getpgrp ();
    }

    enum ErrorNumber
    {
        Interrupted = 4,
        Again = 11,
        InvalidValue = 22,
    }

    [Flags]
    enum DirectionFlags
    {
        None = 0,
        Write = 1,
        Read = 2
    }

    [Flags]
    enum OpenFlags
    {
        ReadOnly = 0x0000,
        WriteOnly = 0x0001,
        ReadWrite = 0x0002,
        NonBlock = 0x0800,
        CloseOnExec = 0x0080000
    }

    [StructLayout(LayoutKind.Sequential)]
    struct Stat
    {
        public IntPtr dev;     /* ID of device containing file */
        public IntPtr ino;     /* inode number */
        public IntPtr mode;    /* protection */
        public IntPtr nlink;   /* number of hard links */
        public IntPtr uid;     /* user ID of owner */
        public IntPtr gid;     /* group ID of owner */
        public IntPtr rdev;    /* device ID (if special file) */
        public IntPtr size;    /* total size, in bytes */
        public IntPtr blksize; /* blocksize for file system I/O */
        public IntPtr blocks;  /* number of 512B blocks allocated */
        public IntPtr atime;   /* time of last access */
        public IntPtr mtime;   /* time of last modification */
        public IntPtr ctime;   /* time of last status change */
    }

    struct EvdevInputId
    {
        public ushort BusType;
        public ushort Vendor;
        public ushort Product;
        public ushort Version;
    }

}

