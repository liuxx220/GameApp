

































#pragma once





















































































#pragma once






























































































































































































































































































































































































































































































































































































































































































































#pragma region Input Buffer SAL 1 compatibility macros




































































































































































































































































































































































































































































                                                




                                                

















































































































































































































































































































#pragma endregion Input Buffer SAL 1 compatibility macros



































































































































































































































































































































































































































































































































































































































































































































































































































































































































extern "C" {









































































































































































































































    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    

    
    
    
    

    
    
























































































































































































































































    
    





















































































}





















#pragma once


extern "C" {











































































































































































































































































































































}











#pragma pack(push,8)














#pragma once














#pragma pack(push,8)


extern "C" {


















typedef __w64 unsigned int   uintptr_t;








typedef char *  va_list;

















































































}


#pragma pack(pop)





extern "C" {














































































































































  








































































































































































































  









  
  










   












  









  





























typedef __w64 unsigned int   size_t;






typedef size_t rsize_t;








typedef __w64 int            intptr_t;

















typedef __w64 int            ptrdiff_t;










typedef unsigned short wint_t;
typedef unsigned short wctype_t;




















typedef int errno_t;



typedef __w64 long __time32_t;   




typedef __int64 __time64_t;     







typedef __time64_t time_t;      






















































  void __cdecl _invalid_parameter(  const wchar_t *,   const wchar_t *,   const wchar_t *, unsigned int, uintptr_t);





 __declspec(noreturn)
void __cdecl _invoke_watson(  const wchar_t *,   const wchar_t *,   const wchar_t *, unsigned int, uintptr_t);



  

















































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































struct threadlocaleinfostruct;
struct threadmbcinfostruct;
typedef struct threadlocaleinfostruct * pthreadlocinfo;
typedef struct threadmbcinfostruct * pthreadmbcinfo;
struct __lc_time_data;

typedef struct localeinfo_struct
{
    pthreadlocinfo locinfo;
    pthreadmbcinfo mbcinfo;
} _locale_tstruct, *_locale_t;


typedef struct localerefcount {
        char *locale;
        wchar_t *wlocale;
        int *refcount;
        int *wrefcount;
} locrefcount;

typedef struct threadlocaleinfostruct {
        int refcount;
        unsigned int lc_codepage;
        unsigned int lc_collate_cp;
        unsigned int lc_time_cp;
        locrefcount lc_category[6];
        int lc_clike;
        int mb_cur_max;
        int * lconv_intl_refcount;
        int * lconv_num_refcount;
        int * lconv_mon_refcount;
        struct lconv * lconv;
        int * ctype1_refcount;
        unsigned short * ctype1;
        const unsigned short * pctype;
        const unsigned char * pclmap;
        const unsigned char * pcumap;
        struct __lc_time_data * lc_time_curr;
        wchar_t * locale_name[6];
} threadlocinfo;




}































#pragma pack(pop)






extern "C" {























 void *  __cdecl _memccpy(   void * _Dst,   const void * _Src,   int _Val,   size_t _MaxCount);
   const void *  __cdecl memchr(   const void * _Buf ,   int _Val,   size_t _MaxCount);
   int     __cdecl _memicmp(  const void * _Buf1,   const void * _Buf2,   size_t _Size);
   int     __cdecl _memicmp_l(  const void * _Buf1,   const void * _Buf2,   size_t _Size,   _locale_t _Locale);
  int     __cdecl memcmp(  const void * _Buf1,   const void * _Buf2,   size_t _Size);

 

void *  __cdecl memcpy(  void * _Dst,   const void * _Src,   size_t _Size);

 errno_t  __cdecl memcpy_s(  void * _Dst,   rsize_t _DstSize,   const void * _Src,   rsize_t _MaxCount);


































         
        
        void *  __cdecl memset(  void * _Dst,   int _Val,   size_t _Size);



__declspec(deprecated("The POSIX name for this item is deprecated. Instead, use the ISO C++ conformant name: " "_memccpy" ". See online help for details."))  void * __cdecl memccpy(  void * _Dst,   const void * _Src,   int _Val,   size_t _Size);
  __declspec(deprecated("The POSIX name for this item is deprecated. Instead, use the ISO C++ conformant name: " "_memicmp" ". See online help for details."))  int __cdecl memicmp(  const void * _Buf1,   const void * _Buf2,   size_t _Size);





  errno_t __cdecl _strset_s(  char * _Dst,   size_t _DstSize,   int _Value);
extern "C++" { template <size_t _Size> inline errno_t __cdecl _strset_s(  char (&_Dest)[_Size],   int _Value) throw() { return _strset_s(_Dest, _Size, _Value); } }
__declspec(deprecated("This function or variable may be unsafe. Consider using " "_strset_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  char * __cdecl _strset( char *_Dest,  int _Value);

  errno_t __cdecl strcpy_s(  char * _Dst,   rsize_t _SizeInBytes,   const char * _Src);

extern "C++" { template <size_t _Size> inline errno_t __cdecl strcpy_s(  char (&_Dest)[_Size],   const char * _Source) throw() { return strcpy_s(_Dest, _Size, _Source); } }
__declspec(deprecated("This function or variable may be unsafe. Consider using " "strcpy_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  char * __cdecl strcpy( char *_Dest,  const char * _Source);

  errno_t __cdecl strcat_s(  char * _Dst,   rsize_t _SizeInBytes,   const char * _Src);

extern "C++" { template <size_t _Size> inline errno_t __cdecl strcat_s(char (&_Dest)[_Size],   const char * _Source) throw() { return strcat_s(_Dest, _Size, _Source); } }

__declspec(deprecated("This function or variable may be unsafe. Consider using " "strcat_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  char * __cdecl strcat( char *_Dest,  const char * _Source);

  int     __cdecl strcmp(  const char * _Str1,   const char * _Str2);
  size_t  __cdecl strlen(  const char * _Str);
  


size_t  __cdecl strnlen(  const char * _Str,   size_t _MaxCount);

  static __inline


size_t  __cdecl strnlen_s(  const char * _Str,   size_t _MaxCount)
{
    return (_Str==0) ? 0 : strnlen(_Str, _MaxCount);
}


  errno_t __cdecl memmove_s(  void * _Dst,   rsize_t _DstSize,   const void * _Src,   rsize_t _MaxCount);


  void *  __cdecl memmove(  void * _Dst,   const void * _Src,   size_t _Size);






   char *  __cdecl _strdup(  const char * _Src);





   const char *  __cdecl strchr(  const char * _Str,   int _Val);
   int     __cdecl _stricmp(   const char * _Str1,    const char * _Str2);
   int     __cdecl _strcmpi(   const char * _Str1,    const char * _Str2);
   int     __cdecl _stricmp_l(   const char * _Str1,    const char * _Str2,   _locale_t _Locale);
   int     __cdecl strcoll(   const char * _Str1,    const  char * _Str2);
   int     __cdecl _strcoll_l(   const char * _Str1,    const char * _Str2,   _locale_t _Locale);
   int     __cdecl _stricoll(   const char * _Str1,    const char * _Str2);
   int     __cdecl _stricoll_l(   const char * _Str1,    const char * _Str2,   _locale_t _Locale);
   int     __cdecl _strncoll  (  const char * _Str1,   const char * _Str2,   size_t _MaxCount);
   int     __cdecl _strncoll_l(  const char * _Str1,   const char * _Str2,   size_t _MaxCount,   _locale_t _Locale);
   int     __cdecl _strnicoll (  const char * _Str1,   const char * _Str2,   size_t _MaxCount);
   int     __cdecl _strnicoll_l(  const char * _Str1,   const char * _Str2,   size_t _MaxCount,   _locale_t _Locale);
   size_t  __cdecl strcspn(   const char * _Str,    const char * _Control);
  __declspec(deprecated("This function or variable may be unsafe. Consider using " "_strerror_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  char *  __cdecl _strerror(  const char * _ErrMsg);
  errno_t __cdecl _strerror_s(  char * _Buf,   size_t _SizeInBytes,   const char * _ErrMsg);
extern "C++" { template <size_t _Size> inline errno_t __cdecl _strerror_s(char (&_Buffer)[_Size],   const char * _ErrorMessage) throw() { return _strerror_s(_Buffer, _Size, _ErrorMessage); } }
  __declspec(deprecated("This function or variable may be unsafe. Consider using " "strerror_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  char *  __cdecl strerror(  int);

  errno_t __cdecl strerror_s(  char * _Buf,   size_t _SizeInBytes,   int _ErrNum);

extern "C++" { template <size_t _Size> inline errno_t __cdecl strerror_s(char (&_Buffer)[_Size],   int _ErrorMessage) throw() { return strerror_s(_Buffer, _Size, _ErrorMessage); } }
  errno_t __cdecl _strlwr_s(  char * _Str,   size_t _Size);
extern "C++" { template <size_t _Size> inline errno_t __cdecl _strlwr_s(  char (&_String)[_Size]) throw() { return _strlwr_s(_String, _Size); } }
__declspec(deprecated("This function or variable may be unsafe. Consider using " "_strlwr_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  char * __cdecl _strlwr( char *_String);
  errno_t __cdecl _strlwr_s_l(  char * _Str,   size_t _Size,   _locale_t _Locale);
extern "C++" { template <size_t _Size> inline errno_t __cdecl _strlwr_s_l(  char (&_String)[_Size],   _locale_t _Locale) throw() { return _strlwr_s_l(_String, _Size, _Locale); } }
__declspec(deprecated("This function or variable may be unsafe. Consider using " "_strlwr_s_l" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  char * __cdecl _strlwr_l(  char *_String,   _locale_t _Locale);

  errno_t __cdecl strncat_s(  char * _Dst,   rsize_t _SizeInBytes,   const char * _Src,   rsize_t _MaxCount);

extern "C++" { template <size_t _Size> inline errno_t __cdecl strncat_s(  char (&_Dest)[_Size],   const char * _Source,   size_t _Count) throw() { return strncat_s(_Dest, _Size, _Source, _Count); } }
#pragma warning(push)
#pragma warning(disable:6059)

__declspec(deprecated("This function or variable may be unsafe. Consider using " "strncat_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  char * __cdecl strncat(  char *_Dest,   const char * _Source,   size_t _Count);
#pragma warning(pop)
   int     __cdecl strncmp(  const char * _Str1,   const char * _Str2,   size_t _MaxCount);
   int     __cdecl _strnicmp(  const char * _Str1,   const char * _Str2,   size_t _MaxCount);
   int     __cdecl _strnicmp_l(  const char * _Str1,   const char * _Str2,   size_t _MaxCount,   _locale_t _Locale);

  errno_t __cdecl strncpy_s(  char * _Dst,   rsize_t _SizeInBytes,   const char * _Src,   rsize_t _MaxCount);

extern "C++" { template <size_t _Size> inline errno_t __cdecl strncpy_s(char (&_Dest)[_Size],   const char * _Source,   size_t _Count) throw() { return strncpy_s(_Dest, _Size, _Source, _Count); } }
__declspec(deprecated("This function or variable may be unsafe. Consider using " "strncpy_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  char * __cdecl strncpy(    char *_Dest,   const char * _Source,   size_t _Count);
  errno_t __cdecl _strnset_s(  char * _Str,   size_t _SizeInBytes,   int _Val,   size_t _MaxCount);
extern "C++" { template <size_t _Size> inline errno_t __cdecl _strnset_s(  char (&_Dest)[_Size],   int _Val,   size_t _Count) throw() { return _strnset_s(_Dest, _Size, _Val, _Count); } }
__declspec(deprecated("This function or variable may be unsafe. Consider using " "_strnset_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  char * __cdecl _strnset(  char *_Dest,   int _Val,   size_t _Count);
   const char *  __cdecl strpbrk(  const char * _Str,   const char * _Control);
   const char *  __cdecl strrchr(  const char * _Str,   int _Ch);
 char *  __cdecl _strrev(  char * _Str);
   size_t  __cdecl strspn(  const char * _Str,   const char * _Control);
     const char *  __cdecl strstr(  const char * _Str,   const char * _SubStr);
  __declspec(deprecated("This function or variable may be unsafe. Consider using " "strtok_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  char *  __cdecl strtok(  char * _Str,   const char * _Delim);

   char *  __cdecl strtok_s(  char * _Str,   const char * _Delim,     char ** _Context);

  errno_t __cdecl _strupr_s(  char * _Str,   size_t _Size);
extern "C++" { template <size_t _Size> inline errno_t __cdecl _strupr_s(  char (&_String)[_Size]) throw() { return _strupr_s(_String, _Size); } }
__declspec(deprecated("This function or variable may be unsafe. Consider using " "_strupr_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  char * __cdecl _strupr( char *_String);
  errno_t __cdecl _strupr_s_l(  char * _Str,   size_t _Size, _locale_t _Locale);
extern "C++" { template <size_t _Size> inline errno_t __cdecl _strupr_s_l(  char (&_String)[_Size], _locale_t _Locale) throw() { return _strupr_s_l(_String, _Size, _Locale); } }
__declspec(deprecated("This function or variable may be unsafe. Consider using " "_strupr_s_l" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  char * __cdecl _strupr_l(  char *_String,   _locale_t _Locale);
  size_t  __cdecl strxfrm (    char * _Dst,   const char * _Src,   size_t _MaxCount);
  size_t  __cdecl _strxfrm_l(    char * _Dst,   const char * _Src,   size_t _MaxCount,   _locale_t _Locale);


extern "C++" {


  inline char * __cdecl strchr(  char * _Str,   int _Ch)
        { return (char*)strchr((const char*)_Str, _Ch); }
  inline char * __cdecl strpbrk(  char * _Str,   const char * _Control)
        { return (char*)strpbrk((const char*)_Str, _Control); }
  inline char * __cdecl strrchr(  char * _Str,   int _Ch)
        { return (char*)strrchr((const char*)_Str, _Ch); }
    inline char * __cdecl strstr(  char * _Str,   const char * _SubStr)
        { return (char*)strstr((const char*)_Str, _SubStr); }



  inline void * __cdecl memchr(  void * _Pv,   int _C,   size_t _N)
        { return (void*)memchr((const void*)_Pv, _C, _N); }

}









  __declspec(deprecated("The POSIX name for this item is deprecated. Instead, use the ISO C++ conformant name: " "_strdup" ". See online help for details."))  char * __cdecl strdup(  const char * _Src);






  __declspec(deprecated("The POSIX name for this item is deprecated. Instead, use the ISO C++ conformant name: " "_strcmpi" ". See online help for details."))  int __cdecl strcmpi(  const char * _Str1,   const char * _Str2);
  __declspec(deprecated("The POSIX name for this item is deprecated. Instead, use the ISO C++ conformant name: " "_stricmp" ". See online help for details."))  int __cdecl stricmp(  const char * _Str1,   const char * _Str2);
__declspec(deprecated("The POSIX name for this item is deprecated. Instead, use the ISO C++ conformant name: " "_strlwr" ". See online help for details."))  char * __cdecl strlwr(  char * _Str);
  __declspec(deprecated("The POSIX name for this item is deprecated. Instead, use the ISO C++ conformant name: " "_strnicmp" ". See online help for details."))  int __cdecl strnicmp(  const char * _Str1,   const char * _Str,   size_t _MaxCount);
__declspec(deprecated("The POSIX name for this item is deprecated. Instead, use the ISO C++ conformant name: " "_strnset" ". See online help for details."))  char * __cdecl strnset(  char * _Str,   int _Val,   size_t _MaxCount);
__declspec(deprecated("The POSIX name for this item is deprecated. Instead, use the ISO C++ conformant name: " "_strrev" ". See online help for details."))  char * __cdecl strrev(  char * _Str);
__declspec(deprecated("The POSIX name for this item is deprecated. Instead, use the ISO C++ conformant name: " "_strset" ". See online help for details."))         char * __cdecl strset(  char * _Str,   int _Val);
__declspec(deprecated("The POSIX name for this item is deprecated. Instead, use the ISO C++ conformant name: " "_strupr" ". See online help for details."))  char * __cdecl strupr(  char * _Str);













   wchar_t * __cdecl _wcsdup(  const wchar_t * _Str);






  errno_t __cdecl wcscat_s(  wchar_t * _Dst,   rsize_t _SizeInWords,   const wchar_t * _Src);

extern "C++" { template <size_t _Size> inline errno_t __cdecl wcscat_s(wchar_t (&_Dest)[_Size],   const wchar_t * _Source) throw() { return wcscat_s(_Dest, _Size, _Source); } }

__declspec(deprecated("This function or variable may be unsafe. Consider using " "wcscat_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  wchar_t * __cdecl wcscat( wchar_t *_Dest,  const wchar_t * _Source);

 

 const wchar_t * __cdecl wcschr(  const wchar_t * _Str, wchar_t _Ch);
   int __cdecl wcscmp(  const wchar_t * _Str1,   const wchar_t * _Str2);

  errno_t __cdecl wcscpy_s(  wchar_t * _Dst,   rsize_t _SizeInWords,   const wchar_t * _Src);

extern "C++" { template <size_t _Size> inline errno_t __cdecl wcscpy_s(wchar_t (&_Dest)[_Size],   const wchar_t * _Source) throw() { return wcscpy_s(_Dest, _Size, _Source); } }
__declspec(deprecated("This function or variable may be unsafe. Consider using " "wcscpy_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  wchar_t * __cdecl wcscpy( wchar_t *_Dest,  const wchar_t * _Source);
   size_t __cdecl wcscspn(  const wchar_t * _Str,   const wchar_t * _Control);
   size_t __cdecl wcslen(  const wchar_t * _Str);
  


size_t __cdecl wcsnlen(  const wchar_t * _Src,   size_t _MaxCount);

  static __inline


size_t __cdecl wcsnlen_s(  const wchar_t * _Src,   size_t _MaxCount)
{
    return (_Src == 0) ? 0 : wcsnlen(_Src, _MaxCount);
}


  errno_t __cdecl wcsncat_s(  wchar_t * _Dst,   rsize_t _SizeInWords,   const wchar_t * _Src,   rsize_t _MaxCount);

extern "C++" { template <size_t _Size> inline errno_t __cdecl wcsncat_s(  wchar_t (&_Dest)[_Size],   const wchar_t * _Source,   size_t _Count) throw() { return wcsncat_s(_Dest, _Size, _Source, _Count); } }
#pragma warning(push)
#pragma warning(disable:6059)
__declspec(deprecated("This function or variable may be unsafe. Consider using " "wcsncat_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  wchar_t * __cdecl wcsncat(  wchar_t *_Dest,   const wchar_t * _Source,   size_t _Count);
#pragma warning(pop)
   int __cdecl wcsncmp(  const wchar_t * _Str1,   const wchar_t * _Str2,   size_t _MaxCount);

  errno_t __cdecl wcsncpy_s(  wchar_t * _Dst,   rsize_t _SizeInWords,   const wchar_t * _Src,   rsize_t _MaxCount);

extern "C++" { template <size_t _Size> inline errno_t __cdecl wcsncpy_s(wchar_t (&_Dest)[_Size],   const wchar_t * _Source,   size_t _Count) throw() { return wcsncpy_s(_Dest, _Size, _Source, _Count); } }
__declspec(deprecated("This function or variable may be unsafe. Consider using " "wcsncpy_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  wchar_t * __cdecl wcsncpy(    wchar_t *_Dest,   const wchar_t * _Source,   size_t _Count);
   const wchar_t * __cdecl wcspbrk(  const wchar_t * _Str,   const wchar_t * _Control);
   const wchar_t * __cdecl wcsrchr(  const wchar_t * _Str,   wchar_t _Ch);
   size_t __cdecl wcsspn(  const wchar_t * _Str,   const wchar_t * _Control);
   

 const wchar_t * __cdecl wcsstr(  const wchar_t * _Str,   const wchar_t * _SubStr);
  __declspec(deprecated("This function or variable may be unsafe. Consider using " "wcstok_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  wchar_t * __cdecl wcstok(  wchar_t * _Str,   const wchar_t * _Delim);

   wchar_t * __cdecl wcstok_s(  wchar_t * _Str,   const wchar_t * _Delim,     wchar_t ** _Context);

  __declspec(deprecated("This function or variable may be unsafe. Consider using " "_wcserror_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  wchar_t * __cdecl _wcserror(  int _ErrNum);
  errno_t __cdecl _wcserror_s(  wchar_t * _Buf,   size_t _SizeInWords,   int _ErrNum);
extern "C++" { template <size_t _Size> inline errno_t __cdecl _wcserror_s(wchar_t (&_Buffer)[_Size],   int _Error) throw() { return _wcserror_s(_Buffer, _Size, _Error); } }
  __declspec(deprecated("This function or variable may be unsafe. Consider using " "__wcserror_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  wchar_t * __cdecl __wcserror(  const wchar_t * _Str);
  errno_t __cdecl __wcserror_s(  wchar_t * _Buffer,   size_t _SizeInWords,   const wchar_t * _ErrMsg);
extern "C++" { template <size_t _Size> inline errno_t __cdecl __wcserror_s(wchar_t (&_Buffer)[_Size],   const wchar_t * _ErrorMessage) throw() { return __wcserror_s(_Buffer, _Size, _ErrorMessage); } }

   int __cdecl _wcsicmp(  const wchar_t * _Str1,   const wchar_t * _Str2);
   int __cdecl _wcsicmp_l(  const wchar_t * _Str1,   const wchar_t * _Str2,   _locale_t _Locale);
   int __cdecl _wcsnicmp(  const wchar_t * _Str1,   const wchar_t * _Str2,   size_t _MaxCount);
   int __cdecl _wcsnicmp_l(  const wchar_t * _Str1,   const wchar_t * _Str2,   size_t _MaxCount,   _locale_t _Locale);
  errno_t __cdecl _wcsnset_s(  wchar_t * _Dst,   size_t _SizeInWords,   wchar_t _Val,   size_t _MaxCount);
extern "C++" { template <size_t _Size> inline errno_t __cdecl _wcsnset_s(  wchar_t (&_Dst)[_Size], wchar_t _Val,   size_t _MaxCount) throw() { return _wcsnset_s(_Dst, _Size, _Val, _MaxCount); } }
__declspec(deprecated("This function or variable may be unsafe. Consider using " "_wcsnset_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  wchar_t * __cdecl _wcsnset(  wchar_t *_Str, wchar_t _Val,   size_t _MaxCount);
 wchar_t * __cdecl _wcsrev(  wchar_t * _Str);
  errno_t __cdecl _wcsset_s(  wchar_t * _Dst,   size_t _SizeInWords,   wchar_t _Value);
extern "C++" { template <size_t _Size> inline errno_t __cdecl _wcsset_s(  wchar_t (&_Str)[_Size], wchar_t _Val) throw() { return _wcsset_s(_Str, _Size, _Val); } }
__declspec(deprecated("This function or variable may be unsafe. Consider using " "_wcsset_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  wchar_t * __cdecl _wcsset(  wchar_t *_Str, wchar_t _Val);

  errno_t __cdecl _wcslwr_s(  wchar_t * _Str,   size_t _SizeInWords);
extern "C++" { template <size_t _Size> inline errno_t __cdecl _wcslwr_s(  wchar_t (&_String)[_Size]) throw() { return _wcslwr_s(_String, _Size); } }
__declspec(deprecated("This function or variable may be unsafe. Consider using " "_wcslwr_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  wchar_t * __cdecl _wcslwr( wchar_t *_String);
  errno_t __cdecl _wcslwr_s_l(  wchar_t * _Str,   size_t _SizeInWords,   _locale_t _Locale);
extern "C++" { template <size_t _Size> inline errno_t __cdecl _wcslwr_s_l(  wchar_t (&_String)[_Size],   _locale_t _Locale) throw() { return _wcslwr_s_l(_String, _Size, _Locale); } }
__declspec(deprecated("This function or variable may be unsafe. Consider using " "_wcslwr_s_l" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  wchar_t * __cdecl _wcslwr_l(  wchar_t *_String,   _locale_t _Locale);
  errno_t __cdecl _wcsupr_s(  wchar_t * _Str,   size_t _Size);
extern "C++" { template <size_t _Size> inline errno_t __cdecl _wcsupr_s(  wchar_t (&_String)[_Size]) throw() { return _wcsupr_s(_String, _Size); } }
__declspec(deprecated("This function or variable may be unsafe. Consider using " "_wcsupr_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  wchar_t * __cdecl _wcsupr( wchar_t *_String);
  errno_t __cdecl _wcsupr_s_l(  wchar_t * _Str,   size_t _Size,   _locale_t _Locale);
extern "C++" { template <size_t _Size> inline errno_t __cdecl _wcsupr_s_l(  wchar_t (&_String)[_Size],   _locale_t _Locale) throw() { return _wcsupr_s_l(_String, _Size, _Locale); } }
__declspec(deprecated("This function or variable may be unsafe. Consider using " "_wcsupr_s_l" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  wchar_t * __cdecl _wcsupr_l(  wchar_t *_String,   _locale_t _Locale);
  size_t __cdecl wcsxfrm(    wchar_t * _Dst,   const wchar_t * _Src,   size_t _MaxCount);
  size_t __cdecl _wcsxfrm_l(    wchar_t * _Dst,   const wchar_t *_Src,   size_t _MaxCount,   _locale_t _Locale);
   int __cdecl wcscoll(  const wchar_t * _Str1,   const wchar_t * _Str2);
   int __cdecl _wcscoll_l(  const wchar_t * _Str1,   const wchar_t * _Str2,   _locale_t _Locale);
   int __cdecl _wcsicoll(  const wchar_t * _Str1,   const wchar_t * _Str2);
   int __cdecl _wcsicoll_l(  const wchar_t * _Str1,   const wchar_t *_Str2,   _locale_t _Locale);
   int __cdecl _wcsncoll(  const wchar_t * _Str1,   const wchar_t * _Str2,   size_t _MaxCount);
   int __cdecl _wcsncoll_l(  const wchar_t * _Str1,   const wchar_t * _Str2,   size_t _MaxCount,   _locale_t _Locale);
   int __cdecl _wcsnicoll(  const wchar_t * _Str1,   const wchar_t * _Str2,   size_t _MaxCount);
   int __cdecl _wcsnicoll_l(  const wchar_t * _Str1,   const wchar_t * _Str2,   size_t _MaxCount,   _locale_t _Locale);




extern "C++" {
 

        inline wchar_t * __cdecl wcschr(  wchar_t *_Str, wchar_t _Ch)
        {return ((wchar_t *)wcschr((const wchar_t *)_Str, _Ch)); }
  inline wchar_t * __cdecl wcspbrk(  wchar_t *_Str,   const wchar_t *_Control)
        {return ((wchar_t *)wcspbrk((const wchar_t *)_Str, _Control)); }
  inline wchar_t * __cdecl wcsrchr(  wchar_t *_Str,   wchar_t _Ch)
        {return ((wchar_t *)wcsrchr((const wchar_t *)_Str, _Ch)); }
   

        inline wchar_t * __cdecl wcsstr(  wchar_t *_Str,   const wchar_t *_SubStr)
        {return ((wchar_t *)wcsstr((const wchar_t *)_Str, _SubStr)); }
}










  __declspec(deprecated("The POSIX name for this item is deprecated. Instead, use the ISO C++ conformant name: " "_wcsdup" ". See online help for details."))  wchar_t * __cdecl wcsdup(  const wchar_t * _Str);









  __declspec(deprecated("The POSIX name for this item is deprecated. Instead, use the ISO C++ conformant name: " "_wcsicmp" ". See online help for details."))  int __cdecl wcsicmp(  const wchar_t * _Str1,   const wchar_t * _Str2);
  __declspec(deprecated("The POSIX name for this item is deprecated. Instead, use the ISO C++ conformant name: " "_wcsnicmp" ". See online help for details."))  int __cdecl wcsnicmp(  const wchar_t * _Str1,   const wchar_t * _Str2,   size_t _MaxCount);
__declspec(deprecated("The POSIX name for this item is deprecated. Instead, use the ISO C++ conformant name: " "_wcsnset" ". See online help for details."))  wchar_t * __cdecl wcsnset(  wchar_t * _Str,   wchar_t _Val,   size_t _MaxCount);
__declspec(deprecated("The POSIX name for this item is deprecated. Instead, use the ISO C++ conformant name: " "_wcsrev" ". See online help for details."))  wchar_t * __cdecl wcsrev(  wchar_t * _Str);
__declspec(deprecated("The POSIX name for this item is deprecated. Instead, use the ISO C++ conformant name: " "_wcsset" ". See online help for details."))  wchar_t * __cdecl wcsset(  wchar_t * _Str, wchar_t _Val);
__declspec(deprecated("The POSIX name for this item is deprecated. Instead, use the ISO C++ conformant name: " "_wcslwr" ". See online help for details."))  wchar_t * __cdecl wcslwr(  wchar_t * _Str);
__declspec(deprecated("The POSIX name for this item is deprecated. Instead, use the ISO C++ conformant name: " "_wcsupr" ". See online help for details."))  wchar_t * __cdecl wcsupr(  wchar_t * _Str);
  __declspec(deprecated("The POSIX name for this item is deprecated. Instead, use the ISO C++ conformant name: " "_wcsicoll" ". See online help for details."))  int __cdecl wcsicoll(  const wchar_t * _Str1,   const wchar_t * _Str2);













}































































































enum dtAllocHint
{
	DT_ALLOC_PERM,		
	DT_ALLOC_TEMP		
};






typedef void* (dtAllocFunc)(int size, dtAllocHint hint);




typedef void (dtFreeFunc)(void* ptr);




void dtAllocSetCustom(dtAllocFunc *allocFunc, dtFreeFunc *freeFunc);






void* dtAlloc(int size, dtAllocHint hint);




void dtFree(void* ptr);

























typedef unsigned int dtStatus;


static const unsigned int DT_FAILURE = 1u << 31;			
static const unsigned int DT_SUCCESS = 1u << 30;			
static const unsigned int DT_IN_PROGRESS = 1u << 29;		


static const unsigned int DT_STATUS_DETAIL_MASK = 0x0ffffff;
static const unsigned int DT_WRONG_MAGIC = 1 << 0;		
static const unsigned int DT_WRONG_VERSION = 1 << 1;	
static const unsigned int DT_OUT_OF_MEMORY = 1 << 2;	
static const unsigned int DT_INVALID_PARAM = 1 << 3;	
static const unsigned int DT_BUFFER_TOO_SMALL = 1 << 4;	
static const unsigned int DT_OUT_OF_NODES = 1 << 5;		
static const unsigned int DT_PARTIAL_RESULT = 1 << 6;	



inline bool dtStatusSucceed(dtStatus status)
{
	return (status & DT_SUCCESS) != 0;
}


inline bool dtStatusFailed(dtStatus status)
{
	return (status & DT_FAILURE) != 0;
}


inline bool dtStatusInProgress(dtStatus status)
{
	return (status & DT_IN_PROGRESS) != 0;
}


inline bool dtStatusDetail(dtStatus status, unsigned int detail)
{
	return (status & detail) != 0;
}









typedef unsigned int dtPolyRef;



typedef unsigned int dtTileRef;



static const int DT_VERTS_PER_POLYGON = 6;








static const int DT_NAVMESH_MAGIC = 'D'<<24 | 'N'<<16 | 'A'<<8 | 'V';


static const int DT_NAVMESH_VERSION = 7;


static const int DT_NAVMESH_STATE_MAGIC = 'D'<<24 | 'N'<<16 | 'M'<<8 | 'S';


static const int DT_NAVMESH_STATE_VERSION = 1;





static const unsigned short DT_EXT_LINK = 0x8000;


static const unsigned int DT_NULL_LINK = 0xffffffff;


static const unsigned int DT_OFFMESH_CON_BIDIR = 1;



static const int DT_MAX_AREAS = 64;



enum dtTileFlags
{
	
	DT_TILE_FREE_DATA = 0x01,
};


enum dtStraightPathFlags
{
	DT_STRAIGHTPATH_START = 0x01,				
	DT_STRAIGHTPATH_END = 0x02,					
	DT_STRAIGHTPATH_OFFMESH_CONNECTION = 0x04,	
};


enum dtStraightPathOptions
{
	DT_STRAIGHTPATH_AREA_CROSSINGS = 0x01,	
	DT_STRAIGHTPATH_ALL_CROSSINGS = 0x02,	
};


enum dtPolyTypes
{
	
	DT_POLYTYPE_GROUND = 0,
	
	DT_POLYTYPE_OFFMESH_CONNECTION = 1,
};




struct dtPoly
{
	
	unsigned int firstLink;

	
	
	unsigned short verts[DT_VERTS_PER_POLYGON];

	
	unsigned short neis[DT_VERTS_PER_POLYGON];

	
	unsigned short flags;

	
	unsigned char vertCount;

	
	
	unsigned char areaAndtype;

	
	inline void setArea(unsigned char a) { areaAndtype = (areaAndtype & 0xc0) | (a & 0x3f); }

	
	inline void setType(unsigned char t) { areaAndtype = (areaAndtype & 0x3f) | (t << 6); }

	
	inline unsigned char getArea() const { return areaAndtype & 0x3f; }

	
	inline unsigned char getType() const { return areaAndtype >> 6; }
};


struct dtPolyDetail
{
	unsigned int vertBase;			
	unsigned int triBase;			
	unsigned char vertCount;		
	unsigned char triCount;			
};




struct dtLink
{
	dtPolyRef ref;					
	unsigned int next;				
	unsigned char edge;				
	unsigned char side;				
	unsigned char bmin;				
	unsigned char bmax;				
};




struct dtBVNode
{
	unsigned short bmin[3];			
	unsigned short bmax[3];			
	int i;							
};



struct dtOffMeshConnection
{
	
	float pos[6];

	
	float rad;		

	
	unsigned short poly;

	
	
	
	unsigned char flags;

	
	unsigned char side;

	
	unsigned int userId;
};



struct dtMeshHeader
{
	int magic;				
	int version;			
	int x;					
	int y;					
	int layer;				
	unsigned int userId;	
	int polyCount;			
	int vertCount;			
	int maxLinkCount;		
	int detailMeshCount;	
	
	
	int detailVertCount;
	
	int detailTriCount;			
	int bvNodeCount;			
	int offMeshConCount;		
	int offMeshBase;			
	float walkableHeight;		
	float walkableRadius;		
	float walkableClimb;		
	float bmin[3];				
	float bmax[3];				
	
	
	float bvQuantFactor;
};



struct dtMeshTile
{
	unsigned int salt;					

	unsigned int linksFreeList;			
	dtMeshHeader* header;				
	dtPoly* polys;						
	float* verts;						
	dtLink* links;						
	dtPolyDetail* detailMeshes;			
	
	
	float* detailVerts;	

	
	unsigned char* detailTris;	

	
	
	dtBVNode* bvTree;

	dtOffMeshConnection* offMeshCons;		
		
	unsigned char* data;					
	int dataSize;							
	int flags;								
	dtMeshTile* next;						
};





struct dtNavMeshParams
{
	float orig[3];					
	float tileWidth;				
	float tileHeight;				
	int maxTiles;					
	int maxPolys;					
};



class dtNavMesh
{
public:
	dtNavMesh();
	~dtNavMesh();

	
	

	
	
	
	dtStatus init(const dtNavMeshParams* params);

	
	
	
	
	
	
	dtStatus init(unsigned char* data, const int dataSize, const int flags);
	
	
	const dtNavMeshParams* getParams() const;

	
	
	
	
	
	
	
	dtStatus addTile(unsigned char* data, int dataSize, int flags, dtTileRef lastRef, dtTileRef* result);
	
	
	
	
	
	
	dtStatus removeTile(dtTileRef ref, unsigned char** data, int* dataSize);

	

	
	

	
	
	
	
	void calcTileLoc(const float* pos, int* tx, int* ty) const;

	
	
	
	
	
	const dtMeshTile* getTileAt(const int x, const int y, const int layer) const;

	
	
	
	
	
	
	int getTilesAt(const int x, const int y,
				   dtMeshTile const** tiles, const int maxTiles) const;
	
	
	
	
	
	
	dtTileRef getTileRefAt(int x, int y, int layer) const;

	
	
	
	dtTileRef getTileRef(const dtMeshTile* tile) const;

	
	
	
	
	const dtMeshTile* getTileByRef(dtTileRef ref) const;
	
	
	
	int getMaxTiles() const;
	
	
	
	
	const dtMeshTile* getTile(int i) const;

	
	
	
	
	
	dtStatus getTileAndPolyByRef(const dtPolyRef ref, const dtMeshTile** tile, const dtPoly** poly) const;
	
	
	
	
	
	void getTileAndPolyByRefUnsafe(const dtPolyRef ref, const dtMeshTile** tile, const dtPoly** poly) const;

	
	
	
	bool isValidPolyRef(dtPolyRef ref) const;
	
	
	
	
	dtPolyRef getPolyRefBase(const dtMeshTile* tile) const;
	
	
	
	
	
	
	
	dtStatus getOffMeshConnectionPolyEndPoints(dtPolyRef prevRef, dtPolyRef polyRef, float* startPos, float* endPos) const;

	
	
	
	const dtOffMeshConnection* getOffMeshConnectionByRef(dtPolyRef ref) const;
	
	

	
	
	

	
	
	
	
	dtStatus setPolyFlags(dtPolyRef ref, unsigned short flags);

	
	
	
	
	dtStatus getPolyFlags(dtPolyRef ref, unsigned short* resultFlags) const;

	
	
	
	
	dtStatus setPolyArea(dtPolyRef ref, unsigned char area);

	
	
	
	
	dtStatus getPolyArea(dtPolyRef ref, unsigned char* resultArea) const;

	
	
	
	int getTileStateSize(const dtMeshTile* tile) const;
	
	
	
	
	
	
	dtStatus storeTileState(const dtMeshTile* tile, unsigned char* data, const int maxDataSize) const;
	
	
	
	
	
	
	dtStatus restoreTileState(dtMeshTile* tile, const unsigned char* data, const int maxDataSize);
	
	

	
	
	

	
	
	
	
	
	inline dtPolyRef encodePolyId(unsigned int salt, unsigned int it, unsigned int ip) const
	{
		return ((dtPolyRef)salt << (m_polyBits+m_tileBits)) | ((dtPolyRef)it << m_polyBits) | (dtPolyRef)ip;
	}
	
	
	
	
	
	
	
	
	inline void decodePolyId(dtPolyRef ref, unsigned int& salt, unsigned int& it, unsigned int& ip) const
	{
		const dtPolyRef saltMask = ((dtPolyRef)1<<m_saltBits)-1;
		const dtPolyRef tileMask = ((dtPolyRef)1<<m_tileBits)-1;
		const dtPolyRef polyMask = ((dtPolyRef)1<<m_polyBits)-1;
		salt = (unsigned int)((ref >> (m_polyBits+m_tileBits)) & saltMask);
		it = (unsigned int)((ref >> m_polyBits) & tileMask);
		ip = (unsigned int)(ref & polyMask);
	}

	
	
	
	
	inline unsigned int decodePolyIdSalt(dtPolyRef ref) const
	{
		const dtPolyRef saltMask = ((dtPolyRef)1<<m_saltBits)-1;
		return (unsigned int)((ref >> (m_polyBits+m_tileBits)) & saltMask);
	}
	
	
	
	
	
	inline unsigned int decodePolyIdTile(dtPolyRef ref) const
	{
		const dtPolyRef tileMask = ((dtPolyRef)1<<m_tileBits)-1;
		return (unsigned int)((ref >> m_polyBits) & tileMask);
	}
	
	
	
	
	
	inline unsigned int decodePolyIdPoly(dtPolyRef ref) const
	{
		const dtPolyRef polyMask = ((dtPolyRef)1<<m_polyBits)-1;
		return (unsigned int)(ref & polyMask);
	}

	
	
private:

	
	dtMeshTile* getTile(int i);

	
	int getTilesAt(const int x, const int y,
				   dtMeshTile** tiles, const int maxTiles) const;

	
	int getNeighbourTilesAt(const int x, const int y, const int side,
							dtMeshTile** tiles, const int maxTiles) const;
	
	
	int findConnectingPolys(const float* va, const float* vb,
							const dtMeshTile* tile, int side,
							dtPolyRef* con, float* conarea, int maxcon) const;
	
	
	void connectIntLinks(dtMeshTile* tile);
	
	void baseOffMeshLinks(dtMeshTile* tile);

	
	void connectExtLinks(dtMeshTile* tile, dtMeshTile* target, int side);
	
	void connectExtOffMeshLinks(dtMeshTile* tile, dtMeshTile* target, int side);
	
	
	void unconnectExtLinks(dtMeshTile* tile, dtMeshTile* target);
	

	
	
	
	int queryPolygonsInTile(const dtMeshTile* tile, const float* qmin, const float* qmax,
							dtPolyRef* polys, const int maxPolys) const;
	
	dtPolyRef findNearestPolyInTile(const dtMeshTile* tile, const float* center,
									const float* extents, float* nearestPt) const;
	
	void closestPointOnPolyInTile(const dtMeshTile* tile, unsigned int ip,
								  const float* pos, float* closest) const;
	
	dtNavMeshParams m_params;			
	float m_orig[3];					
	float m_tileWidth, m_tileHeight;	
	int m_maxTiles;						
	int m_tileLutSize;					
	int m_tileLutMask;					

	dtMeshTile** m_posLookup;			
	dtMeshTile* m_nextFree;				
	dtMeshTile* m_tiles;				
		
	unsigned int m_saltBits;			
	unsigned int m_tileBits;			
	unsigned int m_polyBits;			
};




dtNavMesh* dtAllocNavMesh();




void dtFreeNavMesh(dtNavMesh* navmesh);














































































































class dtQueryFilter
{
	float m_areaCost[DT_MAX_AREAS];		
	unsigned short m_includeFlags;		
	unsigned short m_excludeFlags;		
	
public:
	dtQueryFilter();
	
	
	
	
	





	bool passFilter(const dtPolyRef ref,
					const dtMeshTile* tile,
					const dtPoly* poly) const;


	
	
	
	
	
	
	
	
	
	
	
	
	






	float getCost(const float* pa, const float* pb,
				  const dtPolyRef prevRef, const dtMeshTile* prevTile, const dtPoly* prevPoly,
				  const dtPolyRef curRef, const dtMeshTile* curTile, const dtPoly* curPoly,
				  const dtPolyRef nextRef, const dtMeshTile* nextTile, const dtPoly* nextPoly) const;


	
	

	
	
	
	inline float getAreaCost(const int i) const { return m_areaCost[i]; }

	
	
	
	inline void setAreaCost(const int i, const float cost) { m_areaCost[i] = cost; } 

	
	
	
	inline unsigned short getIncludeFlags() const { return m_includeFlags; }

	
	
	inline void setIncludeFlags(const unsigned short flags) { m_includeFlags = flags; }

	
	
	
	inline unsigned short getExcludeFlags() const { return m_excludeFlags; }

	
	
	inline void setExcludeFlags(const unsigned short flags) { m_excludeFlags = flags; }	

	

};




class dtNavMeshQuery
{
public:
	dtNavMeshQuery();
	~dtNavMeshQuery();
	
	
	
	
	
	dtStatus init(const dtNavMesh* nav, const int maxNodes);
	
	
	

	
	
	
	
	
	
	
	
	
	
	dtStatus findPath(dtPolyRef startRef, dtPolyRef endRef,
					  const float* startPos, const float* endPos,
					  const dtQueryFilter* filter,
					  dtPolyRef* path, int* pathCount, const int maxPath) const;
	
	
	
	
	
	
	
	
	
	
	
	
	
	dtStatus findStraightPath(const float* startPos, const float* endPos,
							  const dtPolyRef* path, const int pathSize,
							  float* straightPath, unsigned char* straightPathFlags, dtPolyRef* straightPathRefs,
							  int* straightPathCount, const int maxStraightPath, const int options = 0) const;

	
	
	
	
	
	
	

	
	
	
	
	
	
	
	dtStatus initSlicedFindPath(dtPolyRef startRef, dtPolyRef endRef,
								const float* startPos, const float* endPos,
								const dtQueryFilter* filter);

	
	
	
	
	dtStatus updateSlicedFindPath(const int maxIter, int* doneIters);

	
	
	
	
	
	
	dtStatus finalizeSlicedFindPath(dtPolyRef* path, int* pathCount, const int maxPath);
	
	
	
	
	
	
	
	
	
	
	dtStatus finalizeSlicedFindPathPartial(const dtPolyRef* existing, const int existingSize,
										   dtPolyRef* path, int* pathCount, const int maxPath);

	
	
	

	
	
	
	
	
	
	
	
	
	
	
	
	dtStatus findPolysAroundCircle(dtPolyRef startRef, const float* centerPos, const float radius,
								   const dtQueryFilter* filter,
								   dtPolyRef* resultRef, dtPolyRef* resultParent, float* resultCost,
								   int* resultCount, const int maxResult) const;
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	dtStatus findPolysAroundShape(dtPolyRef startRef, const float* verts, const int nverts,
								  const dtQueryFilter* filter,
								  dtPolyRef* resultRef, dtPolyRef* resultParent, float* resultCost,
								  int* resultCount, const int maxResult) const;
	
	
	
	

	
	
	
	
	
	
	
	dtStatus findNearestPoly(const float* center, const float* extents,
							 const dtQueryFilter* filter,
							 dtPolyRef* nearestRef, float* nearestPt) const;
	
	
	
	
	
	
	
	
	
	dtStatus queryPolygons(const float* center, const float* extents,
						   const dtQueryFilter* filter,
						   dtPolyRef* polys, int* polyCount, const int maxPolys) const;

	
	
	
	
	
	
	
	
	
	
	
	dtStatus findLocalNeighbourhood(dtPolyRef startRef, const float* centerPos, const float radius,
									const dtQueryFilter* filter,
									dtPolyRef* resultRef, dtPolyRef* resultParent,
									int* resultCount, const int maxResult) const;

	
	
	
	
	
	
	
	
	
	
	dtStatus moveAlongSurface(dtPolyRef startRef, const float* startPos, const float* endPos,
							  const dtQueryFilter* filter,
							  float* resultPos, dtPolyRef* visited, int* visitedCount, const int maxVisitedSize) const;
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	dtStatus raycast(dtPolyRef startRef, const float* startPos, const float* endPos,
					 const dtQueryFilter* filter,
					 float* t, float* hitNormal, dtPolyRef* path, int* pathCount, const int maxPath) const;
	
	
	
	
	
	
	
	
	
	
	
	dtStatus findDistanceToWall(dtPolyRef startRef, const float* centerPos, const float maxRadius,
								const dtQueryFilter* filter,
								float* hitDist, float* hitPos, float* hitNormal) const;
	
	
	
	
	
	
	
	
	
	
	dtStatus getPolyWallSegments(dtPolyRef ref, const dtQueryFilter* filter,
								 float* segmentVerts, dtPolyRef* segmentRefs, int* segmentCount,
								 const int maxSegments) const;

	
	
	
	
	
	
	
	dtStatus findRandomPoint(const dtQueryFilter* filter, float (*frand)(),
							 dtPolyRef* randomRef, float* randomPt) const;

	
	
	
	
	
	
	
	
	
	
	dtStatus findRandomPointAroundCircle(dtPolyRef startRef, const float* centerPos, const float maxRadius,
										 const dtQueryFilter* filter, float (*frand)(),
										 dtPolyRef* randomRef, float* randomPt) const;
	
	
	
	
	
	
	dtStatus closestPointOnPoly(dtPolyRef ref, const float* pos, float* closest) const;
	
	
	
	
	
	
	
	dtStatus closestPointOnPolyBoundary(dtPolyRef ref, const float* pos, float* closest) const;
	
	
	
	
	
	
	dtStatus getPolyHeight(dtPolyRef ref, const float* pos, float* height) const;

	
	
	

	
	
	
	bool isValidPolyRef(dtPolyRef ref, const dtQueryFilter* filter) const;

	
	
	
	bool isInClosedList(dtPolyRef ref) const;
	
	
	
	class dtNodePool* getNodePool() const { return m_nodePool; }
	
	
	
	const dtNavMesh* getAttachedNavMesh() const { return m_nav; }

	
	
private:
	
	
	dtMeshTile* getNeighbourTileAt(int x, int y, int side) const;

	
	int queryPolygonsInTile(const dtMeshTile* tile, const float* qmin, const float* qmax, const dtQueryFilter* filter,
							dtPolyRef* polys, const int maxPolys) const;
	
	dtPolyRef findNearestPolyInTile(const dtMeshTile* tile, const float* center, const float* extents,
									const dtQueryFilter* filter, float* nearestPt) const;
	
	void closestPointOnPolyInTile(const dtMeshTile* tile, const dtPoly* poly, const float* pos, float* closest) const;
	
	
	dtStatus getPortalPoints(dtPolyRef from, dtPolyRef to, float* left, float* right,
							 unsigned char& fromType, unsigned char& toType) const;
	dtStatus getPortalPoints(dtPolyRef from, const dtPoly* fromPoly, const dtMeshTile* fromTile,
							 dtPolyRef to, const dtPoly* toPoly, const dtMeshTile* toTile,
							 float* left, float* right) const;
	
	
	dtStatus getEdgeMidPoint(dtPolyRef from, dtPolyRef to, float* mid) const;
	dtStatus getEdgeMidPoint(dtPolyRef from, const dtPoly* fromPoly, const dtMeshTile* fromTile,
							 dtPolyRef to, const dtPoly* toPoly, const dtMeshTile* toTile,
							 float* mid) const;
	
	
	dtStatus appendVertex(const float* pos, const unsigned char flags, const dtPolyRef ref,
						  float* straightPath, unsigned char* straightPathFlags, dtPolyRef* straightPathRefs,
						  int* straightPathCount, const int maxStraightPath) const;

	
	dtStatus appendPortals(const int startIdx, const int endIdx, const float* endPos, const dtPolyRef* path,
						   float* straightPath, unsigned char* straightPathFlags, dtPolyRef* straightPathRefs,
						   int* straightPathCount, const int maxStraightPath, const int options) const;
	
	const dtNavMesh* m_nav;				

	struct dtQueryData
	{
		dtStatus status;
		struct dtNode* lastBestNode;
		float lastBestNodeCost;
		dtPolyRef startRef, endRef;
		float startPos[3], endPos[3];
		const dtQueryFilter* filter;
	};
	dtQueryData m_query;				

	class dtNodePool* m_tinyNodePool;	
	class dtNodePool* m_nodePool;		
	class dtNodeQueue* m_openList;		
};




dtNavMeshQuery* dtAllocNavMeshQuery();




void dtFreeNavMeshQuery(dtNavMeshQuery* query);






class dtPathCorridor
{
	float m_pos[3];
	float m_target[3];
	
	dtPolyRef* m_path;
	int m_npath;
	int m_maxPath;
	
public:
	dtPathCorridor();
	~dtPathCorridor();
	
	
	
	
	bool init(const int maxPath);
	
	
	
	
	void reset(dtPolyRef ref, const float* pos);
	
	
	
	
	
	
	
	
	
	
	int findCorners(float* cornerVerts, unsigned char* cornerFlags,
					dtPolyRef* cornerPolys, const int maxCorners,
					dtNavMeshQuery* navquery, const dtQueryFilter* filter);
	
	
	
	
	
	
	void optimizePathVisibility(const float* next, const float pathOptimizationRange,
								dtNavMeshQuery* navquery, const dtQueryFilter* filter);
	
	
	
	
	bool optimizePathTopology(dtNavMeshQuery* navquery, const dtQueryFilter* filter);
	
	bool moveOverOffmeshConnection(dtPolyRef offMeshConRef, dtPolyRef* refs,
								   float* startPos, float* endPos,
								   dtNavMeshQuery* navquery);

	bool fixPathStart(dtPolyRef safeRef, const float* safePos);

	bool trimInvalidPath(dtPolyRef safeRef, const float* safePos,
						 dtNavMeshQuery* navquery, const dtQueryFilter* filter);
	
	
	
	
	
	bool isValid(const int maxLookAhead, dtNavMeshQuery* navquery, const dtQueryFilter* filter);
	
	
	
	
	
	
	void movePosition(const float* npos, dtNavMeshQuery* navquery, const dtQueryFilter* filter);

	
	
	
	
	
	void moveTargetPosition(const float* npos, dtNavMeshQuery* navquery, const dtQueryFilter* filter);
	
	
	
	
	
	void setCorridor(const float* target, const dtPolyRef* polys, const int npath);
	
	
	
	inline const float* getPos() const { return m_pos; }

	
	
	inline const float* getTarget() const { return m_target; }
	
	
	
	inline dtPolyRef getFirstPoly() const { return m_npath ? m_path[0] : 0; }

	
	
	inline dtPolyRef getLastPoly() const { return m_npath ? m_path[m_npath-1] : 0; }
	
	
	
	inline const dtPolyRef* getPath() const { return m_path; }

	
	
	inline int getPathCount() const { return m_npath; } 	
};

int dtMergeCorridorStartMoved(dtPolyRef* path, const int npath, const int maxPath,
							  const dtPolyRef* visited, const int nvisited);

int dtMergeCorridorEndMoved(dtPolyRef* path, const int npath, const int maxPath,
							const dtPolyRef* visited, const int nvisited);

int dtMergeCorridorStartShortcut(dtPolyRef* path, const int npath, const int maxPath,
								 const dtPolyRef* visited, const int nvisited);










































template<class T> inline void dtSwap(T& a, T& b) { T t = a; a = b; b = t; }





template<class T> inline T dtMin(T a, T b) { return a < b ? a : b; }





template<class T> inline T dtMax(T a, T b) { return a > b ? a : b; }




template<class T> inline T dtAbs(T a) { return a < 0 ? -a : a; }




template<class T> inline T dtSqr(T a) { return a*a; }






template<class T> inline T dtClamp(T v, T mn, T mx) { return v < mn ? mn : (v > mx ? mx : v); }




float dtSqrt(float x);









inline void dtVcross(float* dest, const float* v1, const float* v2)
{
	dest[0] = v1[1]*v2[2] - v1[2]*v2[1];
	dest[1] = v1[2]*v2[0] - v1[0]*v2[2];
	dest[2] = v1[0]*v2[1] - v1[1]*v2[0]; 
}





inline float dtVdot(const float* v1, const float* v2)
{
	return v1[0]*v2[0] + v1[1]*v2[1] + v1[2]*v2[2];
}






inline void dtVmad(float* dest, const float* v1, const float* v2, const float s)
{
	dest[0] = v1[0]+v2[0]*s;
	dest[1] = v1[1]+v2[1]*s;
	dest[2] = v1[2]+v2[2]*s;
}






inline void dtVlerp(float* dest, const float* v1, const float* v2, const float t)
{
	dest[0] = v1[0]+(v2[0]-v1[0])*t;
	dest[1] = v1[1]+(v2[1]-v1[1])*t;
	dest[2] = v1[2]+(v2[2]-v1[2])*t;
}





inline void dtVadd(float* dest, const float* v1, const float* v2)
{
	dest[0] = v1[0]+v2[0];
	dest[1] = v1[1]+v2[1];
	dest[2] = v1[2]+v2[2];
}





inline void dtVsub(float* dest, const float* v1, const float* v2)
{
	dest[0] = v1[0]-v2[0];
	dest[1] = v1[1]-v2[1];
	dest[2] = v1[2]-v2[2];
}





inline void dtVscale(float* dest, const float* v, const float t)
{
	dest[0] = v[0]*t;
	dest[1] = v[1]*t;
	dest[2] = v[2]*t;
}




inline void dtVmin(float* mn, const float* v)
{
	mn[0] = dtMin(mn[0], v[0]);
	mn[1] = dtMin(mn[1], v[1]);
	mn[2] = dtMin(mn[2], v[2]);
}




inline void dtVmax(float* mx, const float* v)
{
	mx[0] = dtMax(mx[0], v[0]);
	mx[1] = dtMax(mx[1], v[1]);
	mx[2] = dtMax(mx[2], v[2]);
}






inline void dtVset(float* dest, const float x, const float y, const float z)
{
	dest[0] = x; dest[1] = y; dest[2] = z;
}




inline void dtVcopy(float* dest, const float* a)
{
	dest[0] = a[0];
	dest[1] = a[1];
	dest[2] = a[2];
}




inline float dtVlen(const float* v)
{
	return dtSqrt(v[0]*v[0] + v[1]*v[1] + v[2]*v[2]);
}




inline float dtVlenSqr(const float* v)
{
	return v[0]*v[0] + v[1]*v[1] + v[2]*v[2];
}





inline float dtVdist(const float* v1, const float* v2)
{
	const float dx = v2[0] - v1[0];
	const float dy = v2[1] - v1[1];
	const float dz = v2[2] - v1[2];
	return dtSqrt(dx*dx + dy*dy + dz*dz);
}





inline float dtVdistSqr(const float* v1, const float* v2)
{
	const float dx = v2[0] - v1[0];
	const float dy = v2[1] - v1[1];
	const float dz = v2[2] - v1[2];
	return dx*dx + dy*dy + dz*dz;
}







inline float dtVdist2D(const float* v1, const float* v2)
{
	const float dx = v2[0] - v1[0];
	const float dz = v2[2] - v1[2];
	return dtSqrt(dx*dx + dz*dz);
}





inline float dtVdist2DSqr(const float* v1, const float* v2)
{
	const float dx = v2[0] - v1[0];
	const float dz = v2[2] - v1[2];
	return dx*dx + dz*dz;
}



inline void dtVnormalize(float* v)
{
	float d = 1.0f / dtSqrt(dtSqr(v[0]) + dtSqr(v[1]) + dtSqr(v[2]));
	v[0] *= d;
	v[1] *= d;
	v[2] *= d;
}








inline bool dtVequal(const float* p0, const float* p1)
{
	static const float thr = dtSqr(1.0f/16384.0f);
	const float d = dtVdistSqr(p0, p1);
	return d < thr;
}







inline float dtVdot2D(const float* u, const float* v)
{
	return u[0]*v[0] + u[2]*v[2];
}







inline float dtVperp2D(const float* u, const float* v)
{
	return u[2]*v[0] - u[0]*v[2];
}










inline float dtTriArea2D(const float* a, const float* b, const float* c)
{
	const float abx = b[0] - a[0];
	const float abz = b[2] - a[2];
	const float acx = c[0] - a[0];
	const float acz = c[2] - a[2];
	return acx*abz - abx*acz;
}








inline bool dtOverlapQuantBounds(const unsigned short amin[3], const unsigned short amax[3],
								 const unsigned short bmin[3], const unsigned short bmax[3])
{
	bool overlap = true;
	overlap = (amin[0] > bmax[0] || amax[0] < bmin[0]) ? false : overlap;
	overlap = (amin[1] > bmax[1] || amax[1] < bmin[1]) ? false : overlap;
	overlap = (amin[2] > bmax[2] || amax[2] < bmin[2]) ? false : overlap;
	return overlap;
}








inline bool dtOverlapBounds(const float* amin, const float* amax,
							const float* bmin, const float* bmax)
{
	bool overlap = true;
	overlap = (amin[0] > bmax[0] || amax[0] < bmin[0]) ? false : overlap;
	overlap = (amin[1] > bmax[1] || amax[1] < bmin[1]) ? false : overlap;
	overlap = (amin[2] > bmax[2] || amax[2] < bmin[2]) ? false : overlap;
	return overlap;
}







void dtClosestPtPointTriangle(float* closest, const float* p,
							  const float* a, const float* b, const float* c);







bool dtClosestHeightPointTriangle(const float* p, const float* a, const float* b, const float* c, float& h);

bool dtIntersectSegmentPoly2D(const float* p0, const float* p1,
							  const float* verts, int nverts,
							  float& tmin, float& tmax,
							  int& segMin, int& segMax);

bool dtIntersectSegSeg2D(const float* ap, const float* aq,
						 const float* bp, const float* bq,
						 float& s, float& t);






bool dtPointInPolygon(const float* pt, const float* verts, const int nverts);

bool dtDistancePtPolyEdgesSqr(const float* pt, const float* verts, const int nverts,
							float* ed, float* et);

float dtDistancePtSegSqr2D(const float* pt, const float* p, const float* q, float& t);






void dtCalcPolyCenter(float* tc, const unsigned short* idx, int nidx, const float* verts);







bool dtOverlapPolyPoly2D(const float* polya, const int npolya,
						 const float* polyb, const int npolyb);





inline unsigned int dtNextPow2(unsigned int v)
{
	v--;
	v |= v >> 1;
	v |= v >> 2;
	v |= v >> 4;
	v |= v >> 8;
	v |= v >> 16;
	v++;
	return v;
}

inline unsigned int dtIlog2(unsigned int v)
{
	unsigned int r;
	unsigned int shift;
	r = (v > 0xffff) << 4; v >>= r;
	shift = (v > 0xff) << 3; v >>= shift; r |= shift;
	shift = (v > 0xf) << 2; v >>= shift; r |= shift;
	shift = (v > 0x3) << 1; v >>= shift; r |= shift;
	r |= (v >> 1);
	return r;
}

inline int dtAlign4(int x) { return (x+3) & ~3; }

inline int dtOppositeTile(int side) { return (side+4) & 0x7; }

inline void dtSwapByte(unsigned char* a, unsigned char* b)
{
	unsigned char tmp = *a;
	*a = *b;
	*b = tmp;
}

inline void dtSwapEndian(unsigned short* v)
{
	unsigned char* x = (unsigned char*)v;
	dtSwapByte(x+0, x+1);
}

inline void dtSwapEndian(short* v)
{
	unsigned char* x = (unsigned char*)v;
	dtSwapByte(x+0, x+1);
}

inline void dtSwapEndian(unsigned int* v)
{
	unsigned char* x = (unsigned char*)v;
	dtSwapByte(x+0, x+3); dtSwapByte(x+1, x+2);
}

inline void dtSwapEndian(int* v)
{
	unsigned char* x = (unsigned char*)v;
	dtSwapByte(x+0, x+3); dtSwapByte(x+1, x+2);
}

inline void dtSwapEndian(float* v)
{
	unsigned char* x = (unsigned char*)v;
	dtSwapByte(x+0, x+3); dtSwapByte(x+1, x+2);
}

void dtRandomPointInConvexPoly(const float* pts, const int npts, float* areas,
							   const float s, const float t, float* out);























































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































extern "C" {


 void __cdecl _wassert(  const wchar_t * _Message,   const wchar_t *_File,   unsigned _Line);


}














int dtMergeCorridorStartMoved(dtPolyRef* path, const int npath, const int maxPath,
							  const dtPolyRef* visited, const int nvisited)
{
	int furthestPath = -1;
	int furthestVisited = -1;
	
	
	for (int i = npath-1; i >= 0; --i)
	{
		bool found = false;
		for (int j = nvisited-1; j >= 0; --j)
		{
			if (path[i] == visited[j])
			{
				furthestPath = i;
				furthestVisited = j;
				found = true;
			}
		}
		if (found)
			break;
	}
	
	
	if (furthestPath == -1 || furthestVisited == -1)
		return npath;
	
	
	
	
	const int req = nvisited - furthestVisited;
	const int orig = dtMin(furthestPath+1, npath);
	int size = dtMax(0, npath-orig);
	if (req+size > maxPath)
		size = maxPath-req;
	if (size)
		memmove(path+req, path+orig, size*sizeof(dtPolyRef));
	
	
	for (int i = 0; i < req; ++i)
		path[i] = visited[(nvisited-1)-i];				
	
	return req+size;
}

int dtMergeCorridorEndMoved(dtPolyRef* path, const int npath, const int maxPath,
							const dtPolyRef* visited, const int nvisited)
{
	int furthestPath = -1;
	int furthestVisited = -1;
	
	
	for (int i = 0; i < npath; ++i)
	{
		bool found = false;
		for (int j = nvisited-1; j >= 0; --j)
		{
			if (path[i] == visited[j])
			{
				furthestPath = i;
				furthestVisited = j;
				found = true;
			}
		}
		if (found)
			break;
	}
	
	
	if (furthestPath == -1 || furthestVisited == -1)
		return npath;
	
	
	const int ppos = furthestPath+1;
	const int vpos = furthestVisited+1;
	const int count = dtMin(nvisited-vpos, maxPath-ppos);
	(void)( (!!(ppos+count <= maxPath)) || (_wassert(L"ppos+count <= maxPath", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detourpathcorridor.cpp", 103), 0) );
	if (count)
		memcpy(path+ppos, visited+vpos, sizeof(dtPolyRef)*count);
	
	return ppos+count;
}

int dtMergeCorridorStartShortcut(dtPolyRef* path, const int npath, const int maxPath,
								 const dtPolyRef* visited, const int nvisited)
{
	int furthestPath = -1;
	int furthestVisited = -1;
	
	
	for (int i = npath-1; i >= 0; --i)
	{
		bool found = false;
		for (int j = nvisited-1; j >= 0; --j)
		{
			if (path[i] == visited[j])
			{
				furthestPath = i;
				furthestVisited = j;
				found = true;
			}
		}
		if (found)
			break;
	}
	
	
	if (furthestPath == -1 || furthestVisited == -1)
		return npath;
	
	
	
	
	const int req = furthestVisited;
	if (req <= 0)
		return npath;
	
	const int orig = furthestPath;
	int size = dtMax(0, npath-orig);
	if (req+size > maxPath)
		size = maxPath-req;
	if (size)
		memmove(path+req, path+orig, size*sizeof(dtPolyRef));
	
	
	for (int i = 0; i < req; ++i)
		path[i] = visited[i];
	
	return req+size;
}











































dtPathCorridor::dtPathCorridor() :
	m_path(0),
	m_npath(0),
	m_maxPath(0)
{
}

dtPathCorridor::~dtPathCorridor()
{
	dtFree(m_path);
}




bool dtPathCorridor::init(const int maxPath)
{
	(void)( (!!(!m_path)) || (_wassert(L"!m_path", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detourpathcorridor.cpp", 217), 0) );
	m_path = (dtPolyRef*)dtAlloc(sizeof(dtPolyRef)*maxPath, DT_ALLOC_PERM);
	if (!m_path)
		return false;
	m_npath = 0;
	m_maxPath = maxPath;
	return true;
}





void dtPathCorridor::reset(dtPolyRef ref, const float* pos)
{
	(void)( (!!(m_path)) || (_wassert(L"m_path", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detourpathcorridor.cpp", 232), 0) );
	dtVcopy(m_pos, pos);
	dtVcopy(m_target, pos);
	m_path[0] = ref;
	m_npath = 1;
}













int dtPathCorridor::findCorners(float* cornerVerts, unsigned char* cornerFlags,
							  dtPolyRef* cornerPolys, const int maxCorners,
							  dtNavMeshQuery* navquery, const dtQueryFilter* )
{
	(void)( (!!(m_path)) || (_wassert(L"m_path", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detourpathcorridor.cpp", 255), 0) );
	(void)( (!!(m_npath)) || (_wassert(L"m_npath", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detourpathcorridor.cpp", 256), 0) );
	
	static const float MIN_TARGET_DIST = 0.01f;
	
	int ncorners = 0;
	navquery->findStraightPath(m_pos, m_target, m_path, m_npath,
							   cornerVerts, cornerFlags, cornerPolys, &ncorners, maxCorners);
	
	
	while (ncorners)
	{
		if ((cornerFlags[0] & DT_STRAIGHTPATH_OFFMESH_CONNECTION) ||
			dtVdist2DSqr(&cornerVerts[0], m_pos) > dtSqr(MIN_TARGET_DIST))
			break;
		ncorners--;
		if (ncorners)
		{
			memmove(cornerFlags, cornerFlags+1, sizeof(unsigned char)*ncorners);
			memmove(cornerPolys, cornerPolys+1, sizeof(dtPolyRef)*ncorners);
			memmove(cornerVerts, cornerVerts+3, sizeof(float)*3*ncorners);
		}
	}
	
	
	for (int i = 0; i < ncorners; ++i)
	{
		if (cornerFlags[i] & DT_STRAIGHTPATH_OFFMESH_CONNECTION)
		{
			ncorners = i+1;
			break;
		}
	}
	
	return ncorners;
}



















void dtPathCorridor::optimizePathVisibility(const float* next, const float pathOptimizationRange,
										  dtNavMeshQuery* navquery, const dtQueryFilter* filter)
{
	(void)( (!!(m_path)) || (_wassert(L"m_path", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detourpathcorridor.cpp", 313), 0) );
	
	
	float goal[3];
	dtVcopy(goal, next);
	float dist = dtVdist2D(m_pos, goal);
	
	
	if (dist < 0.01f)
		return;
	
	
	dist = dtMin(dist+0.01f, pathOptimizationRange);
	
	
	float delta[3];
	dtVsub(delta, goal, m_pos);
	dtVmad(goal, m_pos, delta, pathOptimizationRange/dist);
	
	static const int MAX_RES = 32;
	dtPolyRef res[MAX_RES];
	float t, norm[3];
	int nres = 0;
	navquery->raycast(m_path[0], m_pos, goal, filter, &t, norm, res, &nres, MAX_RES);
	if (nres > 1 && t > 0.99f)
	{
		m_npath = dtMergeCorridorStartShortcut(m_path, m_npath, m_maxPath, res, nres);
	}
}











bool dtPathCorridor::optimizePathTopology(dtNavMeshQuery* navquery, const dtQueryFilter* filter)
{
	(void)( (!!(navquery)) || (_wassert(L"navquery", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detourpathcorridor.cpp", 355), 0) );
	(void)( (!!(filter)) || (_wassert(L"filter", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detourpathcorridor.cpp", 356), 0) );
	(void)( (!!(m_path)) || (_wassert(L"m_path", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detourpathcorridor.cpp", 357), 0) );
	
	if (m_npath < 3)
		return false;
	
	static const int MAX_ITER = 32;
	static const int MAX_RES = 32;
	
	dtPolyRef res[MAX_RES];
	int nres = 0;
	navquery->initSlicedFindPath(m_path[0], m_path[m_npath-1], m_pos, m_target, filter);
	navquery->updateSlicedFindPath(MAX_ITER, 0);
	dtStatus status = navquery->finalizeSlicedFindPathPartial(m_path, m_npath, res, &nres, MAX_RES);
	
	if (dtStatusSucceed(status) && nres > 0)
	{
		m_npath = dtMergeCorridorStartShortcut(m_path, m_npath, m_maxPath, res, nres);
		return true;
	}
	
	return false;
}

bool dtPathCorridor::moveOverOffmeshConnection(dtPolyRef offMeshConRef, dtPolyRef* refs,
											   float* startPos, float* endPos,
											   dtNavMeshQuery* navquery)
{
	(void)( (!!(navquery)) || (_wassert(L"navquery", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detourpathcorridor.cpp", 384), 0) );
	(void)( (!!(m_path)) || (_wassert(L"m_path", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detourpathcorridor.cpp", 385), 0) );
	(void)( (!!(m_npath)) || (_wassert(L"m_npath", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detourpathcorridor.cpp", 386), 0) );

	
	dtPolyRef prevRef = 0, polyRef = m_path[0];
	int npos = 0;
	while (npos < m_npath && polyRef != offMeshConRef)
	{
		prevRef = polyRef;
		polyRef = m_path[npos];
		npos++;
	}
	if (npos == m_npath)
	{
		
		return false;
	}
	
	
	for (int i = npos; i < m_npath; ++i)
		m_path[i-npos] = m_path[i];
	m_npath -= npos;

	refs[0] = prevRef;
	refs[1] = polyRef;
	
	const dtNavMesh* nav = navquery->getAttachedNavMesh();
	(void)( (!!(nav)) || (_wassert(L"nav", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detourpathcorridor.cpp", 412), 0) );

	dtStatus status = nav->getOffMeshConnectionPolyEndPoints(refs[0], refs[1], startPos, endPos);
	if (dtStatusSucceed(status))
	{
		dtVcopy(m_pos, endPos);
		return true;
	}

	return false;
}
















void dtPathCorridor::movePosition(const float* npos, dtNavMeshQuery* navquery, const dtQueryFilter* filter)
{
	(void)( (!!(m_path)) || (_wassert(L"m_path", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detourpathcorridor.cpp", 441), 0) );
	(void)( (!!(m_npath)) || (_wassert(L"m_npath", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detourpathcorridor.cpp", 442), 0) );
	
	
	float result[3];
	static const int MAX_VISITED = 16;
	dtPolyRef visited[MAX_VISITED];
	int nvisited = 0;
	navquery->moveAlongSurface(m_path[0], m_pos, npos, filter,
							   result, visited, &nvisited, MAX_VISITED);
	m_npath = dtMergeCorridorStartMoved(m_path, m_npath, m_maxPath, visited, nvisited);
	
	
	float h = m_pos[1];
	navquery->getPolyHeight(m_path[0], result, &h);
	result[1] = h;
	dtVcopy(m_pos, result);
}














void dtPathCorridor::moveTargetPosition(const float* npos, dtNavMeshQuery* navquery, const dtQueryFilter* filter)
{
	(void)( (!!(m_path)) || (_wassert(L"m_path", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detourpathcorridor.cpp", 475), 0) );
	(void)( (!!(m_npath)) || (_wassert(L"m_npath", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detourpathcorridor.cpp", 476), 0) );
	
	
	float result[3];
	static const int MAX_VISITED = 16;
	dtPolyRef visited[MAX_VISITED];
	int nvisited = 0;
	navquery->moveAlongSurface(m_path[m_npath-1], m_target, npos, filter,
							   result, visited, &nvisited, MAX_VISITED);
	m_npath = dtMergeCorridorEndMoved(m_path, m_npath, m_maxPath, visited, nvisited);
	
	
	
	


	
	dtVcopy(m_target, result);
}







void dtPathCorridor::setCorridor(const float* target, const dtPolyRef* path, const int npath)
{
	(void)( (!!(m_path)) || (_wassert(L"m_path", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detourpathcorridor.cpp", 504), 0) );
	(void)( (!!(npath > 0)) || (_wassert(L"npath > 0", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detourpathcorridor.cpp", 505), 0) );
	(void)( (!!(npath < m_maxPath)) || (_wassert(L"npath < m_maxPath", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detourpathcorridor.cpp", 506), 0) );
	
	dtVcopy(m_target, target);
	memcpy(m_path, path, sizeof(dtPolyRef)*npath);
	m_npath = npath;
}

bool dtPathCorridor::fixPathStart(dtPolyRef safeRef, const float* safePos)
{
	(void)( (!!(m_path)) || (_wassert(L"m_path", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detourpathcorridor.cpp", 515), 0) );

	dtVcopy(m_pos, safePos);
	if (m_npath < 3 && m_npath > 0)
	{
		m_path[2] = m_path[m_npath-1];
		m_path[0] = safeRef;
		m_path[1] = 0;
		m_npath = 3;
	}
	else
	{
		m_path[0] = safeRef;
		m_path[1] = 0;
	}
	
	return true;
}

bool dtPathCorridor::trimInvalidPath(dtPolyRef safeRef, const float* safePos,
									 dtNavMeshQuery* navquery, const dtQueryFilter* filter)
{
	(void)( (!!(navquery)) || (_wassert(L"navquery", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detourpathcorridor.cpp", 537), 0) );
	(void)( (!!(filter)) || (_wassert(L"filter", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detourpathcorridor.cpp", 538), 0) );
	(void)( (!!(m_path)) || (_wassert(L"m_path", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detourpathcorridor.cpp", 539), 0) );
	
	
	int n = 0;
	while (n < m_npath && navquery->isValidPolyRef(m_path[n], filter)) {
		n++;
	}
	
	if (n == m_npath)
	{
		
		return true;
	}
	else if (n == 0)
	{
		
		dtVcopy(m_pos, safePos);
		m_path[0] = safeRef;
		m_npath = 1;
	}
	else
	{
		
		m_npath = n;
	}
	
	
	float tgt[3];
	dtVcopy(tgt, m_target);
	navquery->closestPointOnPolyBoundary(m_path[m_npath-1], tgt, m_target);
	
	return true;
}





bool dtPathCorridor::isValid(const int maxLookAhead, dtNavMeshQuery* navquery, const dtQueryFilter* filter)
{
	
	const int n = dtMin(m_npath, maxLookAhead);
	for (int i = 0; i < n; ++i)
	{
		if (!navquery->isValidPolyRef(m_path[i], filter))
			return false;
	}

	return true;
}
