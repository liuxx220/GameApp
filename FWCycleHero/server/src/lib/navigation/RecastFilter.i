






















































































































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









#pragma pack(push,8)


extern "C" {









struct _exception {
        int type;       
        char *name;     
        double arg1;    
        double arg2;    
        double retval;  
        } ;










struct _complex {
        double x,y; 
        } ;










typedef float float_t;
typedef double double_t;





















 extern double _HUGE;





























































































void __cdecl _fperrraise(  int _Except);

short  __cdecl _dclass(  double _X);
short  __cdecl _ldclass(  long double _X);
short  __cdecl _fdclass(  float _X);

int  __cdecl _dsign(  double _X);
int  __cdecl _ldsign(  long double _X);
int  __cdecl _fdsign(  float _X);

int  __cdecl _dpcomp(  double _X,   double _Y);
int  __cdecl _ldpcomp(  long double _X,   long double _Y);
int  __cdecl _fdpcomp(  float _X,   float _Y);

short  __cdecl _dtest(  double *_Px);
short  __cdecl _ldtest(  long double *_Px);
short  __cdecl _fdtest(  float *_Px);

short  __cdecl _d_int(  double *_Px,   short _Xexp);
short  __cdecl _ld_int(  long double *_Px,   short _Xexp);
short  __cdecl _fd_int(  float *_Px,   short _Xexp);

short  __cdecl _dscale(  double *_Px,   long _Lexp);
short  __cdecl _ldscale(  long double *_Px,   long _Lexp);
short  __cdecl _fdscale(  float *_Px,   long _Lexp);

short   __cdecl _dunscale(  short *_Pex,   double *_Px);
short  __cdecl _ldunscale(  short *_Pex,   long double *_Px);
short  __cdecl _fdunscale(  short *_Pex,   float *_Px);

short  __cdecl _dexp(  double *_Px,   double _Y,   long _Eoff);
short  __cdecl _ldexp(  long double *_Px,   long double _Y,   long _Eoff);
short  __cdecl _fdexp(  float *_Px,   float _Y,   long _Eoff);

short  __cdecl _dnorm(  unsigned short *_Ps);
short  __cdecl _fdnorm(  unsigned short *_Ps);

double __cdecl _dpoly(  double _X,   const double *_Tab,   int _N);
long double __cdecl _ldpoly(  long double _X,   const long double *_Tab,   int _N);
float __cdecl _fdpoly(  float _X,   const float *_Tab,   int _N);

double  __cdecl _dlog(  double _X,   int _Baseflag);
long double  __cdecl _ldlog(  long double _X,   int _Baseflag);
float  __cdecl _fdlog(  float _X,   int _Baseflag);

double  __cdecl _dsin(  double _X,   unsigned int _Qoff);
long double  __cdecl _ldsin(  long double _X,   unsigned int _Qoff);
float  __cdecl _fdsin(  float _X,   unsigned int _Qoff);


typedef union
{	
    unsigned short _Sh[8];
    double _Val;
} _double_val;


typedef union
{	
    unsigned short _Sh[8];
    float _Val;
} _float_val;


typedef union
{	
    unsigned short _Sh[8];
    long double _Val;
} _ldouble_val;

typedef union
{	
    unsigned short _Word[8];
    float _Float;
    double _Double;
    long double _Long_double;
} _float_const;

extern const _float_const _Denorm_C,  _Inf_C,  _Nan_C,  _Snan_C, _Hugeval_C;
extern const _float_const _FDenorm_C, _FInf_C, _FNan_C, _FSnan_C;
extern const _float_const _LDenorm_C, _LInf_C, _LNan_C, _LSnan_C;

extern const _float_const _Eps_C,  _Rteps_C;
extern const _float_const _FEps_C, _FRteps_C;
extern const _float_const _LEps_C, _LRteps_C;

extern const double _Zero_C, _Xbig_C;
extern const float _FZero_C, _FXbig_C;
extern const long double _LZero_C, _LXbig_C;





























extern "C++" {

inline __declspec(nothrow) int fpclassify(float _X)
{
    return (_fdtest(&_X));
}

inline __declspec(nothrow) int fpclassify(double _X)
{
    return (_dtest(&_X));
}

inline __declspec(nothrow) int fpclassify(long double _X)
{
    return (_ldtest(&_X));
}

inline __declspec(nothrow) bool signbit(float _X)
{
    return (_fdsign(_X) != 0);
}

inline __declspec(nothrow) bool signbit(double _X)
{
    return (_dsign(_X) != 0);
}

inline __declspec(nothrow) bool signbit(long double _X)
{
    return (_ldsign(_X) != 0);
}

inline __declspec(nothrow) int _fpcomp(float _X, float _Y)
{
    return (_fdpcomp(_X, _Y));
}

inline __declspec(nothrow) int _fpcomp(double _X, double _Y)
{
    return (_dpcomp(_X, _Y));
}

inline __declspec(nothrow) int _fpcomp(long double _X, long double _Y)
{
    return (_ldpcomp(_X, _Y));
}

template<class _Trc, class _Tre> struct _Combined_type
{	
    typedef float _Type;	
};

template<> struct _Combined_type<float, double>
{	
    typedef double _Type;
};

template<> struct _Combined_type<float, long double>
{	
    typedef long double _Type;
};

template<class _Ty, class _T2> struct _Real_widened
{	
    typedef long double _Type;	
};

template<> struct _Real_widened<float, float>
{	
    typedef float _Type;
};

template<> struct _Real_widened<float, double>
{	
    typedef double _Type;
};

template<> struct _Real_widened<double, float>
{	
    typedef double _Type;
};

template<> struct _Real_widened<double, double>
{	
    typedef double _Type;
};

template<class _Ty> struct _Real_type
{	
    typedef double _Type;	
};

template<> struct _Real_type<float>
{	
    typedef float _Type;
};

template<> struct _Real_type<long double>
{	
    typedef long double _Type;
};

template<class _T1, class _T2> inline __declspec(nothrow) int _fpcomp(_T1 _X, _T2 _Y)
{	
    typedef typename _Combined_type<float,
        typename _Real_widened<
        typename _Real_type<_T1>::_Type,
        typename _Real_type<_T2>::_Type>::_Type>::_Type _Tw;
    return (_fpcomp((_Tw)_X, (_Tw)_Y));
}

template<class _Ty> inline __declspec(nothrow) bool isfinite(_Ty _X)
{
    return (fpclassify(_X) <= 0);
}

template<class _Ty> inline __declspec(nothrow) bool isinf(_Ty _X)
{
    return (fpclassify(_X) == 1);
}

template<class _Ty> inline __declspec(nothrow) bool isnan(_Ty _X)
{
    return (fpclassify(_X) == 2);
}

template<class _Ty> inline __declspec(nothrow) bool isnormal(_Ty _X)
{
    return (fpclassify(_X) == (-1));
}

template<class _Ty1, class _Ty2> inline __declspec(nothrow) bool isgreater(_Ty1 _X, _Ty2 _Y)
{
    return ((_fpcomp(_X, _Y) & 4) != 0);
}

template<class _Ty1, class _Ty2> inline __declspec(nothrow) bool isgreaterequal(_Ty1 _X, _Ty2 _Y)
{
    return ((_fpcomp(_X, _Y) & (2 | 4)) != 0);
}

template<class _Ty1, class _Ty2> inline __declspec(nothrow) bool isless(_Ty1 _X, _Ty2 _Y)
{
    return ((_fpcomp(_X, _Y) & 1) != 0);
}

template<class _Ty1, class _Ty2> inline __declspec(nothrow) bool islessequal(_Ty1 _X, _Ty2 _Y)
{
    return ((_fpcomp(_X, _Y) & (1 | 2)) != 0);
}

template<class _Ty1, class _Ty2> inline __declspec(nothrow) bool islessgreater(_Ty1 _X, _Ty2 _Y)
{
    return ((_fpcomp(_X, _Y) & (1 | 4)) != 0);
}

template<class _Ty1, class _Ty2> inline __declspec(nothrow) bool isunordered(_Ty1 _X, _Ty2 _Y)
{
    return (_fpcomp(_X, _Y) == 0);
}

}  






int       __cdecl abs(  int _X);
long      __cdecl labs(  long _X);
long long __cdecl llabs(  long long _X);

double  __cdecl acos(  double _X);
 double __cdecl acosh(  double _X);
double  __cdecl asin(  double _X);
 double __cdecl asinh(  double _X);
double  __cdecl atan(  double _X);
 double __cdecl atanh(  double _X);
double  __cdecl atan2(  double _Y,   double _X);

 double __cdecl cbrt(  double _X);
 double __cdecl copysign(  double _X,   double _Y);
double  __cdecl cos(  double _X);
double  __cdecl cosh(  double _X);
 double __cdecl erf(  double _X);
 double __cdecl erfc(  double _X);
double  __cdecl exp(  double _X);
 double __cdecl exp2(  double _X);
 double __cdecl expm1(  double _X);
 double  __cdecl fabs(  double _X);
 double __cdecl fdim(  double _X,   double _Y);
 double __cdecl fma(  double _X,   double _Y,   double _Z);
 double __cdecl fmax(  double _X,   double _Y);
 double __cdecl fmin(  double _X,   double _Y);
double  __cdecl fmod(  double _X,   double _Y);
 int __cdecl ilogb(  double _X);
 double __cdecl lgamma(  double _X);
 long long __cdecl llrint(  double _X);
 long long __cdecl llround(  double _X);
double  __cdecl log(  double _X);
double  __cdecl log10(  double _X);
 double __cdecl log1p(  double _X);
 double __cdecl log2(  double _X);
 double __cdecl logb(  double _X);
 long __cdecl lrint(  double _X);
 long __cdecl lround(  double _X);
 double __cdecl nan(  const char *);
 double __cdecl nearbyint(  double _X);
 double __cdecl nextafter(  double _X,   double _Y);
 double __cdecl nexttoward(  double _X,   long double _Y);
double  __cdecl pow(  double _X,   double _Y);
 double __cdecl remainder(  double _X,   double _Y);
 double __cdecl remquo(  double _X,   double _Y,   int *_Z);
 double __cdecl rint(  double _X);
 double __cdecl round(  double _X);
 double __cdecl scalbln(  double _X,   long _Y);
 double __cdecl scalbn(  double _X,   int _Y);
double  __cdecl sin(  double _X);
double  __cdecl sinh(  double _X);
  double  __cdecl sqrt(  double _X);
double  __cdecl tan(  double _X);
double  __cdecl tanh(  double _X);
 double __cdecl tgamma(  double _X);
 double __cdecl trunc(  double _X);

   double  __cdecl atof(  const char *_String);
   double  __cdecl _atof_l(  const char *_String,   _locale_t _Locale);

 double  __cdecl _cabs(  struct _complex _Complex_value);
 double  __cdecl ceil(  double _X);

   double __cdecl _chgsign (  double _X);
   double __cdecl _copysign (  double _Number,   double _Sign);

 double  __cdecl floor(  double _X);
 double  __cdecl frexp(  double _X,   int * _Y);
 double  __cdecl _hypot(  double _X,   double _Y);
 double  __cdecl _j0(  double _X );
 double  __cdecl _j1(  double _X );
 double  __cdecl _jn(int _X,   double _Y);
 double  __cdecl ldexp(  double _X,   int _Y);




int     __cdecl _matherr(  struct _exception * _Except);


 double  __cdecl modf(  double _X,   double * _Y);
 double  __cdecl _y0(  double _X);
 double  __cdecl _y1(  double _X);
 double  __cdecl _yn(  int _X,   double _Y);

__inline double __cdecl hypot(  double _X,   double _Y)
{
    return _hypot(_X, _Y);
}


 float __cdecl acoshf(  float _X);
 float __cdecl asinhf(  float _X);
 float __cdecl atanhf(  float _X);
 float __cdecl cbrtf(  float _X);
 float  __cdecl _chgsignf(  float _X);
 float __cdecl copysignf(  float _X,   float _Y);
 float  __cdecl _copysignf(  float _Number,   float _Sign);
 float __cdecl erff(  float _X);
 float __cdecl erfcf(  float _X);
 float __cdecl expm1f(  float _X);
 float __cdecl exp2f(  float _X);
 float __cdecl fdimf(  float _X,   float _Y);
 float __cdecl fmaf(  float _X,   float _Y,   float _Z);
 float __cdecl fmaxf(  float _X,   float _Y);
 float __cdecl fminf(  float _X,   float _Y);
 float  __cdecl _hypotf(  float _X,   float _Y);
 int __cdecl ilogbf(  float _X);
 float __cdecl lgammaf(  float _X);
 long long __cdecl llrintf(  float _X);
 long long __cdecl llroundf(  float _X);
 float __cdecl log1pf(  float _X);
 float __cdecl log2f(  float _X);
 float __cdecl logbf(  float _X);
 long __cdecl lrintf(  float _X);
 long __cdecl lroundf(  float _X);
 float __cdecl nanf(  const char *);
 float __cdecl nearbyintf(  float _X);
 float __cdecl nextafterf(  float _X,   float _Y);
 float __cdecl nexttowardf(  float _X,   long double _Y);
 float __cdecl remainderf(  float _X,   float _Y);
 float __cdecl remquof(  float _X,   float _Y,   int *_Z);
 float __cdecl rintf(  float _X);
 float __cdecl roundf(  float _X);
 float __cdecl scalblnf(  float _X,   long _Y);
 float __cdecl scalbnf(  float _X,   int _Y);
 float __cdecl tgammaf(  float _X);
 float __cdecl truncf(  float _X);



 int  __cdecl _set_SSE2_enable(  int _Flag);



































__inline float  __cdecl acosf(  float _X)
{
    return (float)acos(_X);
}

__inline float  __cdecl asinf(  float _X)
{
    return (float)asin(_X);
}

__inline float  __cdecl atan2f(  float _Y,   float _X)
{
    return (float)atan2(_Y, _X);
}

__inline float  __cdecl atanf(  float _X)
{
    return (float)atan(_X);
}

__inline float  __cdecl ceilf(  float _X)
{
    return (float)ceil(_X);
}

__inline float  __cdecl cosf(  float _X)
{
    return (float)cos(_X);
}

__inline float  __cdecl coshf(  float _X)
{
    return (float)cosh(_X);
}

__inline float  __cdecl expf(  float _X)
{
    return (float)exp(_X);
}









__inline float __cdecl fabsf(  float _X)
{
    return (float)fabs(_X);
}










__inline float __cdecl floorf(  float _X)
{
    return (float)floor(_X);
}

__inline float __cdecl fmodf(  float _X,   float _Y)
{
    return (float)fmod(_X, _Y);
}



__inline float __cdecl frexpf(  float _X,   int *_Y)
{
    return (float)frexp(_X, _Y);
}

__inline float __cdecl hypotf(  float _X,   float _Y)
{
    return _hypotf(_X, _Y);
}

__inline float __cdecl ldexpf(  float _X,   int _Y)
{
    return (float)ldexp(_X, _Y);
}















__inline float __cdecl log10f(  float _X)
{
    return (float)log10(_X);
}

__inline float __cdecl logf(  float _X)
{
    return (float)log(_X);
}

__inline float __cdecl modff(  float _X,   float *_Y)
{
    double _F, _I;
    _F = modf(_X, &_I);
    *_Y = (float)_I;
    return (float)_F;
}

__inline float __cdecl powf(  float _X,   float _Y)
{
    return (float)pow(_X, _Y);
}

__inline float __cdecl sinf(  float _X)
{
    return (float)sin(_X);
}

__inline float __cdecl sinhf(  float _X)
{
    return (float)sinh(_X);
}

__inline float __cdecl sqrtf(  float _X)
{
    return (float)sqrt(_X);
}

__inline float __cdecl tanf(  float _X)
{
    return (float)tan(_X);
}

__inline float __cdecl tanhf(  float _X)
{
    return (float)tanh(_X);
}



 long double __cdecl acoshl(  long double _X);

__inline long double __cdecl acosl(  long double _X)
{
    return acos((double)_X);
}

 long double __cdecl asinhl(  long double _X);

__inline long double __cdecl asinl(  long double _X)
{
    return asin((double)_X);
}

__inline long double __cdecl atan2l(  long double _Y,   long double _X)
{
    return atan2((double)_Y, (double)_X);
}

 long double __cdecl atanhl(  long double _X);

__inline long double __cdecl atanl(  long double _X)
{
    return atan((double)_X);
}

 long double __cdecl cbrtl(  long double _X);

__inline long double __cdecl ceill(  long double _X)
{
    return ceil((double)_X);
}

__inline long double __cdecl _chgsignl(  long double _X)
{
    return _chgsign((double)_X);
}

 long double __cdecl copysignl(  long double _X,   long double _Y);

__inline long double __cdecl _copysignl(  long double _X,   long double _Y)
{
    return _copysign((double)_X, (double)_Y);
}

__inline long double __cdecl coshl(  long double _X)
{
    return cosh((double)_X);
}

__inline long double __cdecl cosl(  long double _X)
{
    return cos((double)_X);
}

 long double __cdecl erfl(  long double _X);
 long double __cdecl erfcl(  long double _X);

__inline long double __cdecl expl(  long double _X)
{
    return exp((double)_X);
}

 long double __cdecl exp2l(  long double _X);
 long double __cdecl expm1l(  long double _X);

__inline long double __cdecl fabsl(  long double _X)
{
    return fabs((double)_X);
}

 long double __cdecl fdiml(  long double _X,   long double _Y);

__inline long double __cdecl floorl(  long double _X)
{
    return floor((double)_X);
}

 long double __cdecl fmal(  long double _X,   long double _Y,   long double _Z);
 long double __cdecl fmaxl(  long double _X,   long double _Y);
 long double __cdecl fminl(  long double _X,   long double _Y);

__inline long double __cdecl fmodl(  long double _X,   long double _Y)
{
    return fmod((double)_X, (double)_Y);
}

__inline long double __cdecl frexpl(  long double _X,   int *_Y)
{
    return frexp((double)_X, _Y);
}

 int __cdecl ilogbl(  long double _X);

__inline long double __cdecl _hypotl(  long double _X,   long double _Y)
{
    return _hypot((double)_X, (double)_Y);
}

__inline long double __cdecl hypotl(  long double _X,   long double _Y)
{
    return _hypot((double)_X, (double)_Y);
}

__inline long double __cdecl ldexpl(  long double _X,   int _Y)
{
    return ldexp((double)_X, _Y);
}

 long double __cdecl lgammal(  long double _X);
 long long __cdecl llrintl(  long double _X);
 long long __cdecl llroundl(  long double _X);

__inline long double __cdecl logl(  long double _X)
{
    return log((double)_X);
}

__inline long double __cdecl log10l(  long double _X)
{
    return log10((double)_X);
}

 long double __cdecl log1pl(  long double _X);
 long double __cdecl log2l(  long double _X);
 long double __cdecl logbl(  long double _X);
 long __cdecl lrintl(  long double _X);
 long __cdecl lroundl(  long double _X);

__inline long double __cdecl modfl(  long double _X,   long double *_Y)
{
    double _F, _I;
    _F = modf((double)_X, &_I);
    *_Y = _I;
    return _F;
}
 long double __cdecl nanl(  const char *);
 long double __cdecl nearbyintl(  long double _X);
 long double __cdecl nextafterl(  long double _X,   long double _Y);
 long double __cdecl nexttowardl(  long double _X,   long double _Y);

__inline long double __cdecl powl(  long double _X,   long double _Y)
{
    return pow((double)_X, (double)_Y);
}

 long double __cdecl remainderl(  long double _X,   long double _Y);
 long double __cdecl remquol(  long double _X,   long double _Y,   int *_Z);
 long double __cdecl rintl(  long double _X);
 long double __cdecl roundl(  long double _X);
 long double __cdecl scalblnl(  long double _X,   long _Y);
 long double __cdecl scalbnl(  long double _X,   int _Y);

__inline long double __cdecl sinhl(  long double _X)
{
    return sinh((double)_X);
}

__inline long double __cdecl sinl(  long double _X)
{
    return sin((double)_X);
}

__inline long double __cdecl sqrtl(  long double _X)
{
    return sqrt((double)_X);
}

__inline long double __cdecl tanhl(  long double _X)
{
    return tanh((double)_X);
}

__inline long double __cdecl tanl(  long double _X)
{
    return tan((double)_X);
}

 long double __cdecl tgammal(  long double _X);
 long double __cdecl truncl(  long double _X);






















 extern double HUGE;




__declspec(deprecated("The POSIX name for this item is deprecated. Instead, use the ISO C++ conformant name: " "_j0" ". See online help for details."))  double  __cdecl j0(  double _X);
__declspec(deprecated("The POSIX name for this item is deprecated. Instead, use the ISO C++ conformant name: " "_j1" ". See online help for details."))  double  __cdecl j1(  double _X);
__declspec(deprecated("The POSIX name for this item is deprecated. Instead, use the ISO C++ conformant name: " "_jn" ". See online help for details."))  double  __cdecl jn(  int _X,   double _Y);
__declspec(deprecated("The POSIX name for this item is deprecated. Instead, use the ISO C++ conformant name: " "_y0" ". See online help for details."))  double  __cdecl y0(  double _X);
__declspec(deprecated("The POSIX name for this item is deprecated. Instead, use the ISO C++ conformant name: " "_y1" ". See online help for details."))  double  __cdecl y1(  double _X);
__declspec(deprecated("The POSIX name for this item is deprecated. Instead, use the ISO C++ conformant name: " "_yn" ". See online help for details."))  double  __cdecl yn(  int _X,   double _Y);





}

extern "C++" {

template<class _Ty> inline
        _Ty _Pow_int(_Ty _X, int _Y) throw()
        {unsigned int _N;
        if (_Y >= 0)
                _N = (unsigned int)_Y;
        else
                _N = (unsigned int)(-_Y);
        for (_Ty _Z = _Ty(1); ; _X *= _X)
                {if ((_N & 1) != 0)
                        _Z *= _X;
                if ((_N >>= 1) == 0)
                        return (_Y < 0 ? _Ty(1) / _Z : _Z); }}

inline double __cdecl abs(  double _X) throw()
        {return (fabs(_X)); }
inline double __cdecl pow(  double _X,   int _Y) throw()
        {return (_Pow_int(_X, _Y)); }
inline float __cdecl abs(  float _X) throw()
        {return (fabsf(_X)); }
inline float __cdecl acos(  float _X) throw()
        {return (acosf(_X)); }
inline float __cdecl acosh(  float _X) throw()
        {return (acoshf(_X)); }
inline float __cdecl asin(  float _X) throw()
        {return (asinf(_X)); }
inline float __cdecl asinh(  float _X) throw()
        {return (asinhf(_X)); }
inline float __cdecl atan(  float _X) throw()
        {return (atanf(_X)); }
inline float __cdecl atanh(  float _X) throw()
        {return (atanhf(_X)); }
inline float __cdecl atan2(  float _Y,   float _X) throw()
        {return (atan2f(_Y, _X)); }
inline float __cdecl cbrt(  float _X) throw()
        {return (cbrtf(_X)); }
inline float __cdecl ceil(  float _X) throw()
        {return (ceilf(_X)); }
inline float __cdecl copysign(  float _X,   float _Y) throw()
        {return (copysignf(_X, _Y)); }
inline float __cdecl cos(  float _X) throw()
        {return (cosf(_X)); }
inline float __cdecl cosh(  float _X) throw()
        {return (coshf(_X)); }
inline float __cdecl erf(  float _X) throw()
        {return (erff(_X)); }
inline float __cdecl erfc(  float _X) throw()
        {return (erfcf(_X)); }
inline float __cdecl exp(  float _X) throw()
        {return (expf(_X)); }
inline float __cdecl exp2(  float _X) throw()
        {return (exp2f(_X)); }
inline float __cdecl expm1(  float _X) throw()
        {return (expm1f(_X)); }
inline float __cdecl fabs(  float _X) throw()
        {return (fabsf(_X)); }
inline float __cdecl fdim(  float _X,   float _Y) throw()
        {return (fdimf(_X, _Y)); }
inline float __cdecl floor(  float _X) throw()
        {return (floorf(_X)); }
inline float __cdecl fma(  float _X,   float _Y,   float _Z) throw()
        {return (fmaf(_X, _Y, _Z)); }
inline float __cdecl fmax(  float _X,   float _Y) throw()
        {return (fmaxf(_X, _Y)); }
inline float __cdecl fmin(  float _X,   float _Y) throw()
        {return (fminf(_X, _Y)); }
inline float __cdecl fmod(  float _X,   float _Y) throw()
        {return (fmodf(_X, _Y)); }
inline float __cdecl frexp(  float _X,   int * _Y) throw()
        {return (frexpf(_X, _Y)); }
inline float __cdecl hypot(  float _X,   float _Y) throw()
        {return (hypotf(_X, _Y)); }
inline int __cdecl ilogb(  float _X) throw()
        {return (ilogbf(_X)); }
inline float __cdecl ldexp(  float _X,   int _Y) throw()
        {return (ldexpf(_X, _Y)); }
inline float __cdecl lgamma(  float _X) throw()
        {return (lgammaf(_X)); }
inline long long __cdecl llrint(  float _X) throw()
        {return (llrintf(_X)); }
inline long long __cdecl llround(  float _X) throw()
        {return (llroundf(_X)); }
inline float __cdecl log(  float _X) throw()
        {return (logf(_X)); }
inline float __cdecl log10(  float _X) throw()
        {return (log10f(_X)); }
inline float __cdecl log1p(  float _X) throw()
        {return (log1pf(_X)); }
inline float __cdecl log2(  float _X) throw()
        {return (log2f(_X)); }
inline float __cdecl logb(  float _X) throw()
        {return (logbf(_X)); }
inline long __cdecl lrint(  float _X) throw()
        {return (lrintf(_X)); }
inline long __cdecl lround(  float _X) throw()
        {return (lroundf(_X)); }
inline float __cdecl modf(  float _X,   float * _Y) throw()
        {return (modff(_X, _Y)); }
inline float __cdecl nearbyint(  float _X) throw()
        {return (nearbyintf(_X)); }
inline float __cdecl nextafter(  float _X,   float _Y) throw()
        {return (nextafterf(_X, _Y)); }
inline float __cdecl nexttoward(  float _X,   long double _Y) throw()
        {return (nexttowardf(_X, _Y)); }
inline float __cdecl pow(  float _X,   float _Y) throw()
        {return (powf(_X, _Y)); }
inline float __cdecl pow(  float _X,   int _Y) throw()
        {return (_Pow_int(_X, _Y)); }
inline float __cdecl remainder(  float _X,   float _Y) throw()
        {return (remainderf(_X, _Y)); }
inline float __cdecl remquo(  float _X,   float _Y,   int *_Z) throw()
        {return (remquof(_X, _Y, _Z)); }
inline float __cdecl rint(  float _X) throw()
        {return (rintf(_X)); }
inline float __cdecl round(  float _X) throw()
        {return (roundf(_X)); }
inline float __cdecl scalbln(  float _X,   long _Y) throw()
        {return (scalblnf(_X, _Y)); }
inline float __cdecl scalbn(  float _X,   int _Y) throw()
        {return (scalbnf(_X, _Y)); }
inline float __cdecl sin(  float _X) throw()
        {return (sinf(_X)); }
inline float __cdecl sinh(  float _X) throw()
        {return (sinhf(_X)); }
inline float __cdecl sqrt(  float _X) throw()
        {return (sqrtf(_X)); }
inline float __cdecl tan(  float _X) throw()
        {return (tanf(_X)); }
inline float __cdecl tanh(  float _X) throw()
        {return (tanhf(_X)); }
inline float __cdecl tgamma(  float _X) throw()
        {return (tgammaf(_X)); }
inline float __cdecl trunc(  float _X) throw()
        {return (truncf(_X)); }
inline long double __cdecl abs(  long double _X) throw()
        {return (fabsl(_X)); }
inline long double __cdecl acos(  long double _X) throw()
        {return (acosl(_X)); }
inline long double __cdecl acosh(  long double _X) throw()
        {return (acoshl(_X)); }
inline long double __cdecl asin(  long double _X) throw()
        {return (asinl(_X)); }
inline long double __cdecl asinh(  long double _X) throw()
        {return (asinhl(_X)); }
inline long double __cdecl atan(  long double _X) throw()
        {return (atanl(_X)); }
inline long double __cdecl atanh(  long double _X) throw()
        {return (atanhl(_X)); }
inline long double __cdecl atan2(  long double _Y,   long double _X) throw()
        {return (atan2l(_Y, _X)); }
inline long double __cdecl cbrt(  long double _X) throw()
        {return (cbrtl(_X)); }
inline long double __cdecl ceil(  long double _X) throw()
        {return (ceill(_X)); }
inline long double __cdecl copysign(  long double _X,   long double _Y) throw()
        {return (copysignl(_X, _Y)); }
inline long double __cdecl cos(  long double _X) throw()
        {return (cosl(_X)); }
inline long double __cdecl cosh(  long double _X) throw()
        {return (coshl(_X)); }
inline long double __cdecl erf(  long double _X) throw()
        {return (erfl(_X)); }
inline long double __cdecl erfc(  long double _X) throw()
        {return (erfcl(_X)); }
inline long double __cdecl exp(  long double _X) throw()
        {return (expl(_X)); }
inline long double __cdecl exp2(  long double _X) throw()
        {return (exp2l(_X)); }
inline long double __cdecl expm1(  long double _X) throw()
        {return (expm1l(_X)); }
inline long double __cdecl fabs(  long double _X) throw()
        {return (fabsl(_X)); }
inline long double __cdecl fdim(  long double _X,   long double _Y) throw()
        {return (fdiml(_X, _Y)); }
inline long double __cdecl floor(  long double _X) throw()
        {return (floorl(_X)); }
inline long double __cdecl fma(  long double _X,   long double _Y,   long double _Z) throw()
        {return (fmal(_X, _Y, _Z)); }
inline long double __cdecl fmax(  long double _X,   long double _Y) throw()
        {return (fmaxl(_X, _Y)); }
inline long double __cdecl fmin(  long double _X,   long double _Y) throw()
        {return (fminl(_X, _Y)); }
inline long double __cdecl fmod(  long double _X,   long double _Y) throw()
        {return (fmodl(_X, _Y)); }
inline long double __cdecl frexp(  long double _X,   int * _Y) throw()
        {return (frexpl(_X, _Y)); }
inline long double __cdecl hypot(  long double _X,   long double _Y) throw()
        {return (hypotl(_X, _Y)); }
inline int __cdecl ilogb(  long double _X) throw()
        {return (ilogbl(_X)); }
inline long double __cdecl ldexp(  long double _X,   int _Y) throw()
        {return (ldexpl(_X, _Y)); }
inline long double __cdecl lgamma(  long double _X) throw()
        {return (lgammal(_X)); }
inline long long __cdecl llrint(  long double _X) throw()
        {return (llrintl(_X)); }
inline long long __cdecl llround(  long double _X) throw()
        {return (llroundl(_X)); }
inline long double __cdecl log(  long double _X) throw()
        {return (logl(_X)); }
inline long double __cdecl log10(  long double _X) throw()
        {return (log10l(_X)); }
inline long double __cdecl log1p(  long double _X) throw()
        {return (log1pl(_X)); }
inline long double __cdecl log2(  long double _X) throw()
        {return (log2l(_X)); }
inline long double __cdecl logb(  long double _X) throw()
        {return (logbl(_X)); }
inline long  __cdecl lrint(  long double _X) throw()
        {return (lrintl(_X)); }
inline long  __cdecl lround(  long double _X) throw()
        {return (lroundl(_X)); }
inline long double __cdecl modf(  long double _X,   long double * _Y) throw()
        {return (modfl(_X, _Y)); }
inline long double __cdecl nearbyint(  long double _X) throw()
        {return (nearbyintl(_X)); }
inline long double __cdecl nextafter(  long double _X,   long double _Y) throw()
        {return (nextafterl(_X, _Y)); }
inline long double __cdecl nexttoward(  long double _X,   long double _Y) throw()
        {return (nexttowardl(_X, _Y)); }
inline long double __cdecl pow(  long double _X,   long double _Y) throw()
        {return (powl(_X, _Y)); }
inline long double __cdecl pow(  long double _X,   int _Y) throw()
        {return (_Pow_int(_X, _Y)); }
inline long double __cdecl remainder(  long double _X,   long double _Y) throw()
        {return (remainderl(_X, _Y)); }
inline long double __cdecl remquo(  long double _X,   long double _Y,   int *_Z) throw()
        {return (remquol(_X, _Y, _Z)); }
inline long double __cdecl rint(  long double _X) throw()
        {return (rintl(_X)); }
inline long double __cdecl round(  long double _X) throw()
        {return (roundl(_X)); }
inline long double __cdecl scalbln(  long double _X,   long _Y) throw()
        {return (scalblnl(_X, _Y)); }
inline long double __cdecl scalbn(  long double _X,   int _Y) throw()
        {return (scalbnl(_X, _Y)); }
inline long double __cdecl sin(  long double _X) throw()
        {return (sinl(_X)); }
inline long double __cdecl sinh(  long double _X) throw()
        {return (sinhl(_X)); }
inline long double __cdecl sqrt(  long double _X) throw()
        {return (sqrtl(_X)); }
inline long double __cdecl tan(  long double _X) throw()
        {return (tanl(_X)); }
inline long double __cdecl tanh(  long double _X) throw()
        {return (tanhl(_X)); }
inline long double __cdecl tgamma(  long double _X) throw()
        {return (tgammal(_X)); }
inline long double __cdecl trunc(  long double _X) throw()
        {return (truncl(_X)); }

}


#pragma pack(pop)



























































#pragma once


















































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































#pragma pack(push,8)


extern "C" {

































struct _iobuf {
        char *_ptr;
        int   _cnt;
        char *_base;
        int   _flag;
        int   _file;
        int   _charbuf;
        int   _bufsiz;
        char *_tmpfname;
        };
typedef struct _iobuf FILE;































































 FILE * __cdecl __iob_func(void);






typedef __int64 fpos_t;






























   int __cdecl _filbuf(  FILE * _File );
  int __cdecl _flsbuf(  int _Ch,   FILE * _File);

   FILE * __cdecl _fsopen(  const char * _Filename,   const char * _Mode,   int _ShFlag);

 void __cdecl clearerr(  FILE * _File);

  errno_t __cdecl clearerr_s(  FILE * _File );

  int __cdecl fclose(  FILE * _File);
  int __cdecl _fcloseall(void);

   FILE * __cdecl _fdopen(  int _FileHandle,   const char * _Mode);

   int __cdecl feof(  FILE * _File);
   int __cdecl ferror(  FILE * _File);
  int __cdecl fflush(  FILE * _File);
  int __cdecl fgetc(  FILE * _File);
  int __cdecl _fgetchar(void);
  int __cdecl fgetpos(  FILE * _File ,   fpos_t * _Pos);
  char * __cdecl fgets(  char * _Buf,   int _MaxCount,   FILE * _File);

   int __cdecl _fileno(  FILE * _File);






   char * __cdecl _tempnam(  const char * _DirName,   const char * _FilePrefix);





  int __cdecl _flushall(void);
  __declspec(deprecated("This function or variable may be unsafe. Consider using " "fopen_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  FILE * __cdecl fopen(  const char * _Filename,   const char * _Mode);

  errno_t __cdecl fopen_s(  FILE ** _File,   const char * _Filename,   const char * _Mode);

  int __cdecl fprintf(  FILE * _File,     const char * _Format, ...);

  int __cdecl fprintf_s(  FILE * _File,     const char * _Format, ...);

  int __cdecl fputc(  int _Ch,   FILE * _File);
  int __cdecl _fputchar(  int _Ch);
  int __cdecl fputs(  const char * _Str,   FILE * _File);
  size_t __cdecl fread(  void * _DstBuf,   size_t _ElementSize,   size_t _Count,   FILE * _File);

  size_t __cdecl fread_s(  void * _DstBuf,   size_t _DstSize,   size_t _ElementSize,   size_t _Count,   FILE * _File);

  __declspec(deprecated("This function or variable may be unsafe. Consider using " "freopen_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  FILE * __cdecl freopen(  const char * _Filename,   const char * _Mode,   FILE * _File);

  errno_t __cdecl freopen_s(  FILE ** _File,   const char * _Filename,   const char * _Mode,   FILE * _OldFile);

  __declspec(deprecated("This function or variable may be unsafe. Consider using " "fscanf_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  int __cdecl fscanf(  FILE * _File,     const char * _Format, ...);
 __declspec(deprecated("This function or variable may be unsafe. Consider using " "_fscanf_s_l" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  int __cdecl _fscanf_l(  FILE * _File,     const char * _Format,   _locale_t _Locale, ...);
#pragma warning(push)
#pragma warning(disable:6530)

  int __cdecl fscanf_s(  FILE * _File,     const char * _Format, ...);

  int __cdecl _fscanf_s_l(  FILE * _File,     const char * _Format,   _locale_t _Locale, ...);
#pragma warning(pop)
  int __cdecl fsetpos(  FILE * _File,   const fpos_t * _Pos);
  int __cdecl fseek(  FILE * _File,   long _Offset,   int _Origin);
   long __cdecl ftell(  FILE * _File);

  int __cdecl _fseeki64(  FILE * _File,   __int64 _Offset,   int _Origin);
   __int64 __cdecl _ftelli64(  FILE * _File);

  size_t __cdecl fwrite(  const void * _Str,   size_t _Size,   size_t _Count,   FILE * _File);
   int __cdecl getc(  FILE * _File);
   int __cdecl getchar(void);
   int __cdecl _getmaxstdio(void);

 char * __cdecl gets_s(  char * _Buf,   rsize_t _Size);

extern "C++" { template <size_t _Size> inline char * __cdecl gets_s(char (&_Buffer)[_Size]) throw() { return gets_s(_Buffer, _Size); } }
__declspec(deprecated("This function or variable may be unsafe. Consider using " "gets_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  char * __cdecl gets(  char *_Buffer);
  int __cdecl _getw(  FILE * _File);


 void __cdecl perror(  const char * _ErrMsg);


  int __cdecl _pclose(  FILE * _File);
   FILE * __cdecl _popen(  const char * _Command,   const char * _Mode);

  int __cdecl printf(    const char * _Format, ...);

  int __cdecl printf_s(    const char * _Format, ...);

  int __cdecl putc(  int _Ch,   FILE * _File);
  int __cdecl putchar(  int _Ch);
  int __cdecl puts(  const char * _Str);
  int __cdecl _putw(  int _Word,   FILE * _File);


 int __cdecl remove(  const char * _Filename);
   int __cdecl rename(  const char * _OldFilename,   const char * _NewFilename);
 int __cdecl _unlink(  const char * _Filename);

__declspec(deprecated("The POSIX name for this item is deprecated. Instead, use the ISO C++ conformant name: " "_unlink" ". See online help for details."))  int __cdecl unlink(  const char * _Filename);


 void __cdecl rewind(  FILE * _File);
  int __cdecl _rmtmp(void);
  __declspec(deprecated("This function or variable may be unsafe. Consider using " "scanf_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  int __cdecl scanf(    const char * _Format, ...);
 __declspec(deprecated("This function or variable may be unsafe. Consider using " "_scanf_s_l" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  int __cdecl _scanf_l(    const char * _Format,   _locale_t _Locale, ...);
#pragma warning(push)
#pragma warning(disable:6530)

  int __cdecl scanf_s(    const char * _Format, ...);

  int __cdecl _scanf_s_l(    const char * _Format,   _locale_t _Locale, ...);
#pragma warning(pop)
__declspec(deprecated("This function or variable may be unsafe. Consider using " "setvbuf" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  void __cdecl setbuf(  FILE * _File,     char * _Buffer);
  int __cdecl _setmaxstdio(  int _Max);
  unsigned int __cdecl _set_output_format(  unsigned int _Format);
  unsigned int __cdecl _get_output_format(void);
  int __cdecl setvbuf(  FILE * _File,   char * _Buf,   int _Mode,   size_t _Size);
  int __cdecl _snprintf_s(  char * _DstBuf,   size_t _SizeInBytes,   size_t _MaxCount,     const char * _Format, ...);
extern "C++" { __pragma(warning(push)); __pragma(warning(disable: 4793)); template <size_t _Size> inline int __cdecl _snprintf_s(  char (&_Dest)[_Size],   size_t _MaxCount,     const char * _Format, ...) throw() { va_list _ArgList; ( _ArgList = (va_list)( &reinterpret_cast<const char &>(_Format) ) + ( (sizeof(_Format) + sizeof(int) - 1) & ~(sizeof(int) - 1) ) ); return _vsnprintf_s(_Dest, _Size, _MaxCount, _Format, _ArgList); } __pragma(warning(pop)); }

  int __cdecl sprintf_s(  char * _DstBuf,   size_t _SizeInBytes,     const char * _Format, ...);

extern "C++" { __pragma(warning(push)); __pragma(warning(disable: 4793)); template <size_t _Size> inline int __cdecl sprintf_s(  char (&_Dest)[_Size],     const char * _Format, ...) throw() { va_list _ArgList; ( _ArgList = (va_list)( &reinterpret_cast<const char &>(_Format) ) + ( (sizeof(_Format) + sizeof(int) - 1) & ~(sizeof(int) - 1) ) ); return vsprintf_s(_Dest, _Size, _Format, _ArgList); } __pragma(warning(pop)); }
   int __cdecl _scprintf(    const char * _Format, ...);
  __declspec(deprecated("This function or variable may be unsafe. Consider using " "sscanf_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  int __cdecl sscanf(  const char * _Src,     const char * _Format, ...);
 __declspec(deprecated("This function or variable may be unsafe. Consider using " "_sscanf_s_l" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  int __cdecl _sscanf_l(  const char * _Src,     const char * _Format,   _locale_t _Locale, ...);
#pragma warning(push)
#pragma warning(disable:6530)

  int __cdecl sscanf_s(  const char * _Src,     const char * _Format, ...);

  int __cdecl _sscanf_s_l(  const char * _Src,     const char * _Format,   _locale_t _Locale, ...);
 __declspec(deprecated("This function or variable may be unsafe. Consider using " "_snscanf_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  int __cdecl _snscanf(    const char * _Src,   size_t _MaxCount,     const char * _Format, ...);
 __declspec(deprecated("This function or variable may be unsafe. Consider using " "_snscanf_s_l" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  int __cdecl _snscanf_l(    const char * _Src,   size_t _MaxCount,     const char * _Format,   _locale_t _Locale, ...);
  int __cdecl _snscanf_s(    const char * _Src,   size_t _MaxCount,     const char * _Format, ...);
  int __cdecl _snscanf_s_l(    const char * _Src,   size_t _MaxCount,     const char * _Format,   _locale_t _Locale, ...);
#pragma warning(pop)
  __declspec(deprecated("This function or variable may be unsafe. Consider using " "tmpfile_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  FILE * __cdecl tmpfile(void);

  errno_t __cdecl tmpfile_s(    FILE ** _File);
  errno_t __cdecl tmpnam_s(  char * _Buf,   rsize_t _Size);

extern "C++" { template <size_t _Size> inline errno_t __cdecl tmpnam_s(  char (&_Buf)[_Size]) throw() { return tmpnam_s(_Buf, _Size); } }
__declspec(deprecated("This function or variable may be unsafe. Consider using " "tmpnam_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  char * __cdecl tmpnam(  char *_Buffer);
  int __cdecl ungetc(  int _Ch,   FILE * _File);
  int __cdecl vfprintf(  FILE * _File,     const char * _Format, va_list _ArgList);
  int __cdecl vfscanf(  FILE * _File,     const char * _Format, va_list _ArgList);

  int __cdecl vfprintf_s(  FILE * _File,     const char * _Format, va_list _ArgList);
  int __cdecl vfscanf_s(  FILE * _File,     const char * _Format, va_list _ArgList);

  int __cdecl vprintf(    const char * _Format, va_list _ArgList);
  int __cdecl vscanf(    const char * _Format, va_list _ArgList);

  int __cdecl vprintf_s(    const char * _Format, va_list _ArgList);
  int __cdecl vscanf_s(    const char * _Format, va_list _ArgList);

 __declspec(deprecated("This function or variable may be unsafe. Consider using " "vsnprintf_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  int __cdecl vsnprintf(  char * _DstBuf,   size_t _MaxCount,     const char * _Format, va_list _ArgList);

  int __cdecl vsnprintf_s(  char * _DstBuf,   size_t _DstSize,   size_t _MaxCount,     const char * _Format, va_list _ArgList);
extern "C++" { template <size_t _Size> inline int __cdecl vsnprintf_s(  char (&_Dest)[_Size],   size_t _MaxCount,     const char * _Format, va_list _Args) throw() { return vsnprintf_s(_Dest, _Size, _MaxCount, _Format, _Args); } }

  int __cdecl _vsnprintf_s(  char * _DstBuf,   size_t _SizeInBytes,   size_t _MaxCount,     const char * _Format, va_list _ArgList);
extern "C++" { template <size_t _Size> inline int __cdecl _vsnprintf_s(  char (&_Dest)[_Size],   size_t _MaxCount,     const char * _Format, va_list _Args) throw() { return _vsnprintf_s(_Dest, _Size, _MaxCount, _Format, _Args); } }
#pragma warning(push)
#pragma warning(disable:4793)
__declspec(deprecated("This function or variable may be unsafe. Consider using " "_snprintf_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  int __cdecl _snprintf(    char *_Dest,   size_t _Count,     const char * _Format, ...); __declspec(deprecated("This function or variable may be unsafe. Consider using " "_vsnprintf_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  int __cdecl _vsnprintf(    char *_Dest,   size_t _Count,     const char * _Format, va_list _Args);
#pragma warning(pop)

 int __cdecl vsprintf_s(  char * _DstBuf,   size_t _SizeInBytes,     const char * _Format, va_list _ArgList);
extern "C++" { template <size_t _Size> inline int __cdecl vsprintf_s(  char (&_Dest)[_Size],     const char * _Format, va_list _Args) throw() { return vsprintf_s(_Dest, _Size, _Format, _Args); } }
  int __cdecl vsscanf_s(const char * _Src,     const char * _Format, va_list _ArgList);
extern "C++" { template <size_t _Size> inline int __cdecl vsscanf_s(  const char (&_Src)[_Size],     const char * _Format, va_list _Args) throw() { return vsscanf_s(_Src, _Size, _Format, _Args); } }

#pragma warning(push)
#pragma warning(disable:4793)
__declspec(deprecated("This function or variable may be unsafe. Consider using " "sprintf_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  int __cdecl sprintf(  char *_Dest,  const char * _Format, ...); __declspec(deprecated("This function or variable may be unsafe. Consider using " "vsprintf_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  int __cdecl vsprintf(  char *_Dest,  const char * _Format, va_list _Args);
  int __cdecl vsscanf(const char * _srcBuf,     const char * _Format, va_list _ArgList);
#pragma warning(pop)
   int __cdecl _vscprintf(    const char * _Format, va_list _ArgList);
  int __cdecl _snprintf_c(  char * _DstBuf,   size_t _MaxCount,     const char * _Format, ...);
  int __cdecl _vsnprintf_c(  char *_DstBuf,   size_t _MaxCount,     const char * _Format, va_list _ArgList);

  int __cdecl _fprintf_p(  FILE * _File,     const char * _Format, ...);
  int __cdecl _printf_p(    const char * _Format, ...);
  int __cdecl _sprintf_p(  char * _Dst,   size_t _MaxCount,     const char * _Format, ...);
  int __cdecl _vfprintf_p(  FILE * _File,     const char * _Format, va_list _ArgList);
  int __cdecl _vprintf_p(    const char * _Format, va_list _ArgList);
  int __cdecl _vsprintf_p(  char * _Dst,   size_t _MaxCount,     const char * _Format, va_list _ArgList);
   int __cdecl _scprintf_p(    const char * _Format, ...);
   int __cdecl _vscprintf_p(    const char * _Format, va_list _ArgList);
 int __cdecl _set_printf_count_output(  int _Value);
 int __cdecl _get_printf_count_output(void);

  int __cdecl _printf_l(    const char * _Format,   _locale_t _Locale, ...);
  int __cdecl _printf_p_l(    const char * _Format,   _locale_t _Locale, ...);
  int __cdecl _printf_s_l(    const char * _Format,   _locale_t _Locale, ...);
  int __cdecl _vprintf_l(    const char * _Format,   _locale_t _Locale, va_list _ArgList);
  int __cdecl _vprintf_p_l(    const char * _Format,   _locale_t _Locale, va_list _ArgList);
  int __cdecl _vprintf_s_l(    const char * _Format,   _locale_t _Locale, va_list _ArgList);

  int __cdecl _fprintf_l(  FILE * _File,     const char * _Format,   _locale_t _Locale, ...);
  int __cdecl _fprintf_p_l(  FILE * _File,     const char * _Format,   _locale_t _Locale, ...);
  int __cdecl _fprintf_s_l(  FILE * _File,     const char * _Format,   _locale_t _Locale, ...);
  int __cdecl _vfprintf_l(  FILE * _File,   const char * _Format,   _locale_t _Locale, va_list _ArgList);
  int __cdecl _vfprintf_p_l(  FILE * _File,   const char * _Format,   _locale_t _Locale, va_list _ArgList);
  int __cdecl _vfprintf_s_l(  FILE * _File,   const char * _Format,   _locale_t _Locale, va_list _ArgList);

 __declspec(deprecated("This function or variable may be unsafe. Consider using " "_sprintf_s_l" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  int __cdecl _sprintf_l(    char * _DstBuf,     const char * _Format,   _locale_t _Locale, ...);
  int __cdecl _sprintf_p_l(  char * _DstBuf,   size_t _MaxCount,     const char * _Format,   _locale_t _Locale, ...);
  int __cdecl _sprintf_s_l(  char * _DstBuf,   size_t _DstSize,     const char * _Format,   _locale_t _Locale, ...);
 __declspec(deprecated("This function or variable may be unsafe. Consider using " "_vsprintf_s_l" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  int __cdecl _vsprintf_l(    char * _DstBuf,   const char * _Format,   _locale_t, va_list _ArgList);
  int __cdecl _vsprintf_p_l(  char * _DstBuf,   size_t _MaxCount,     const char* _Format,   _locale_t _Locale,  va_list _ArgList);
  int __cdecl _vsprintf_s_l(  char * _DstBuf,   size_t _DstSize,     const char * _Format,   _locale_t _Locale, va_list _ArgList);

  int __cdecl _scprintf_l(    const char * _Format,   _locale_t _Locale, ...);
  int __cdecl _scprintf_p_l(    const char * _Format,   _locale_t _Locale, ...);
  int __cdecl _vscprintf_l(    const char * _Format,   _locale_t _Locale, va_list _ArgList);
  int __cdecl _vscprintf_p_l(    const char * _Format,   _locale_t _Locale, va_list _ArgList);

 __declspec(deprecated("This function or variable may be unsafe. Consider using " "_snprintf_s_l" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  int __cdecl _snprintf_l(  char * _DstBuf,   size_t _MaxCount,     const char * _Format,   _locale_t _Locale, ...);
  int __cdecl _snprintf_c_l(  char * _DstBuf,   size_t _MaxCount,     const char * _Format,   _locale_t _Locale, ...);
  int __cdecl _snprintf_s_l(  char * _DstBuf,   size_t _DstSize,   size_t _MaxCount,     const char * _Format,   _locale_t _Locale, ...);
 __declspec(deprecated("This function or variable may be unsafe. Consider using " "_vsnprintf_s_l" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  int __cdecl _vsnprintf_l(  char * _DstBuf,   size_t _MaxCount,     const char * _Format,   _locale_t _Locale, va_list _ArgList);
  int __cdecl _vsnprintf_c_l(  char * _DstBuf,   size_t _MaxCount, const char *,   _locale_t _Locale, va_list _ArgList);
  int __cdecl _vsnprintf_s_l(  char * _DstBuf,   size_t _DstSize,   size_t _MaxCount,     const char* _Format,  _locale_t _Locale, va_list _ArgList);









   FILE * __cdecl _wfsopen(  const wchar_t * _Filename,   const wchar_t * _Mode,   int _ShFlag);

  wint_t __cdecl fgetwc(  FILE * _File);
  wint_t __cdecl _fgetwchar(void);
  wint_t __cdecl fputwc(  wchar_t _Ch,   FILE * _File);
  wint_t __cdecl _fputwchar(  wchar_t _Ch);
   wint_t __cdecl getwc(  FILE * _File);
   wint_t __cdecl getwchar(void);
  wint_t __cdecl putwc(  wchar_t _Ch,   FILE * _File);
  wint_t __cdecl putwchar(  wchar_t _Ch);
  wint_t __cdecl ungetwc(  wint_t _Ch,   FILE * _File);

  wchar_t * __cdecl fgetws(  wchar_t * _Dst,   int _SizeInWords,   FILE * _File);
  int __cdecl fputws(  const wchar_t * _Str,   FILE * _File);
  wchar_t * __cdecl _getws_s(  wchar_t * _Str,   size_t _SizeInWords);
extern "C++" { template <size_t _Size> inline wchar_t * __cdecl _getws_s(  wchar_t (&_String)[_Size]) throw() { return _getws_s(_String, _Size); } }
__declspec(deprecated("This function or variable may be unsafe. Consider using " "_getws_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  wchar_t * __cdecl _getws(  wchar_t *_String);
  int __cdecl _putws(  const wchar_t * _Str);

  int __cdecl fwprintf(  FILE * _File,     const wchar_t * _Format, ...);

  int __cdecl fwprintf_s(  FILE * _File,     const wchar_t * _Format, ...);

  int __cdecl wprintf(    const wchar_t * _Format, ...);

  int __cdecl wprintf_s(    const wchar_t * _Format, ...);

   int __cdecl _scwprintf(    const wchar_t * _Format, ...);
  int __cdecl vfwprintf(  FILE * _File,     const wchar_t * _Format, va_list _ArgList);
  int __cdecl vfwscanf(  FILE * _File,     const wchar_t * _Format, va_list _ArgList);

  int __cdecl vfwprintf_s(  FILE * _File,     const wchar_t * _Format, va_list _ArgList);
  int __cdecl vfwscanf_s(  FILE * _File,     const wchar_t * _Format, va_list _ArgList);

  int __cdecl vwprintf(    const wchar_t * _Format, va_list _ArgList);
  int __cdecl vwscanf(    const wchar_t * _Format, va_list _ArgList);

  int __cdecl vwprintf_s(    const wchar_t * _Format, va_list _ArgList);
  int __cdecl vwscanf_s(    const wchar_t * _Format, va_list _ArgList);



 int __cdecl swprintf_s(  wchar_t * _Dst,   size_t _SizeInWords,     const wchar_t * _Format, ...);

extern "C++" { __pragma(warning(push)); __pragma(warning(disable: 4793)); template <size_t _Size> inline int __cdecl swprintf_s(  wchar_t (&_Dest)[_Size],     const wchar_t * _Format, ...) throw() { va_list _ArgList; ( _ArgList = (va_list)( &reinterpret_cast<const char &>(_Format) ) + ( (sizeof(_Format) + sizeof(int) - 1) & ~(sizeof(int) - 1) ) ); return vswprintf_s(_Dest, _Size, _Format, _ArgList); } __pragma(warning(pop)); }

 int __cdecl vswprintf_s(  wchar_t * _Dst,   size_t _SizeInWords,     const wchar_t * _Format, va_list _ArgList);
  int __cdecl vswscanf_s(const wchar_t * _Src,     const wchar_t * _Format, va_list _ArgList);

extern "C++" { template <size_t _Size> inline int __cdecl vswprintf_s(  wchar_t (&_Dest)[_Size],     const wchar_t * _Format, va_list _Args) throw() { return vswprintf_s(_Dest, _Size, _Format, _Args); } }
extern "C++" { template <size_t _Size> inline int __cdecl vswscanf_s(  wchar_t (&_Dest)[_Size],     const wchar_t * _Format, va_list _Args) throw() { return vswscanf_s(_Dest, _Size, _Format, _Args); } }
  int __cdecl vswscanf(const wchar_t * _srcBuf,     const wchar_t * _Format, va_list _ArgList);

  int __cdecl _swprintf_c(  wchar_t * _DstBuf,   size_t _SizeInWords,     const wchar_t * _Format, ...);
  int __cdecl _vswprintf_c(  wchar_t * _DstBuf,   size_t _SizeInWords,     const wchar_t * _Format, va_list _ArgList);

  int __cdecl _snwprintf_s(  wchar_t * _DstBuf,   size_t _SizeInWords,   size_t _MaxCount,     const wchar_t * _Format, ...);
extern "C++" { __pragma(warning(push)); __pragma(warning(disable: 4793)); template <size_t _Size> inline int __cdecl _snwprintf_s(  wchar_t (&_Dest)[_Size],   size_t _Count,     const wchar_t * _Format, ...) throw() { va_list _ArgList; ( _ArgList = (va_list)( &reinterpret_cast<const char &>(_Format) ) + ( (sizeof(_Format) + sizeof(int) - 1) & ~(sizeof(int) - 1) ) ); return _vsnwprintf_s(_Dest, _Size, _Count, _Format, _ArgList); } __pragma(warning(pop)); }
  int __cdecl _vsnwprintf_s(  wchar_t * _DstBuf,   size_t _SizeInWords,   size_t _MaxCount,     const wchar_t * _Format, va_list _ArgList);
extern "C++" { template <size_t _Size> inline int __cdecl _vsnwprintf_s(  wchar_t (&_Dest)[_Size],   size_t _Count,     const wchar_t * _Format, va_list _Args) throw() { return _vsnwprintf_s(_Dest, _Size, _Count, _Format, _Args); } }
#pragma warning(push)
#pragma warning(disable:4793)
__declspec(deprecated("This function or variable may be unsafe. Consider using " "_snwprintf_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  int __cdecl _snwprintf(    wchar_t *_Dest,   size_t _Count,     const wchar_t * _Format, ...); __declspec(deprecated("This function or variable may be unsafe. Consider using " "_vsnwprintf_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  int __cdecl _vsnwprintf(    wchar_t *_Dest,   size_t _Count,     const wchar_t * _Format, va_list _Args);
#pragma warning(pop)

  int __cdecl _fwprintf_p(  FILE * _File,     const wchar_t * _Format, ...);
  int __cdecl _wprintf_p(    const wchar_t * _Format, ...);
  int __cdecl _vfwprintf_p(  FILE * _File,     const wchar_t * _Format, va_list _ArgList);
  int __cdecl _vwprintf_p(    const wchar_t * _Format, va_list _ArgList);
  int __cdecl _swprintf_p(  wchar_t * _DstBuf,   size_t _MaxCount,     const wchar_t * _Format, ...);
  int __cdecl _vswprintf_p(  wchar_t * _DstBuf,   size_t _MaxCount,     const wchar_t * _Format, va_list _ArgList);
   int __cdecl _scwprintf_p(    const wchar_t * _Format, ...);
   int __cdecl _vscwprintf_p(    const wchar_t * _Format, va_list _ArgList);

  int __cdecl _wprintf_l(    const wchar_t * _Format,   _locale_t _Locale, ...);
  int __cdecl _wprintf_p_l(    const wchar_t * _Format,   _locale_t _Locale, ...);
  int __cdecl _wprintf_s_l(    const wchar_t * _Format,   _locale_t _Locale, ...);
  int __cdecl _vwprintf_l(    const wchar_t * _Format,   _locale_t _Locale, va_list _ArgList);
  int __cdecl _vwprintf_p_l(    const wchar_t * _Format,   _locale_t _Locale, va_list _ArgList);
  int __cdecl _vwprintf_s_l(    const wchar_t * _Format,   _locale_t _Locale, va_list _ArgList);

  int __cdecl _fwprintf_l(  FILE * _File,     const wchar_t * _Format,   _locale_t _Locale, ...);
  int __cdecl _fwprintf_p_l(  FILE * _File,     const wchar_t * _Format,   _locale_t _Locale, ...);
  int __cdecl _fwprintf_s_l(  FILE * _File,     const wchar_t * _Format,   _locale_t _Locale, ...);
  int __cdecl _vfwprintf_l(  FILE * _File,     const wchar_t * _Format,   _locale_t _Locale, va_list _ArgList);
  int __cdecl _vfwprintf_p_l(  FILE * _File,     const wchar_t * _Format,   _locale_t _Locale, va_list _ArgList);
  int __cdecl _vfwprintf_s_l(  FILE * _File,     const wchar_t * _Format,   _locale_t _Locale, va_list _ArgList);

  int __cdecl _swprintf_c_l(  wchar_t * _DstBuf,   size_t _MaxCount,     const wchar_t * _Format,   _locale_t _Locale, ...);
  int __cdecl _swprintf_p_l(  wchar_t * _DstBuf,   size_t _MaxCount,     const wchar_t * _Format,   _locale_t _Locale, ...);
  int __cdecl _swprintf_s_l(  wchar_t * _DstBuf,   size_t _DstSize,     const wchar_t * _Format,   _locale_t _Locale, ...);
  int __cdecl _vswprintf_c_l(  wchar_t * _DstBuf,   size_t _MaxCount,     const wchar_t * _Format,   _locale_t _Locale, va_list _ArgList);
  int __cdecl _vswprintf_p_l(  wchar_t * _DstBuf,   size_t _MaxCount,     const wchar_t * _Format,   _locale_t _Locale, va_list _ArgList);
  int __cdecl _vswprintf_s_l(  wchar_t * _DstBuf,   size_t _DstSize,     const wchar_t * _Format,   _locale_t _Locale, va_list _ArgList);

   int __cdecl _scwprintf_l(    const wchar_t * _Format,   _locale_t _Locale, ...);
   int __cdecl _scwprintf_p_l(    const wchar_t * _Format,   _locale_t _Locale, ...);
   int __cdecl _vscwprintf_p_l(    const wchar_t * _Format,   _locale_t _Locale, va_list _ArgList);

 __declspec(deprecated("This function or variable may be unsafe. Consider using " "_snwprintf_s_l" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  int __cdecl _snwprintf_l(  wchar_t * _DstBuf,   size_t _MaxCount,     const wchar_t * _Format,   _locale_t _Locale, ...);
  int __cdecl _snwprintf_s_l(  wchar_t * _DstBuf,   size_t _DstSize,   size_t _MaxCount,     const wchar_t * _Format,   _locale_t _Locale, ...);
 __declspec(deprecated("This function or variable may be unsafe. Consider using " "_vsnwprintf_s_l" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  int __cdecl _vsnwprintf_l(  wchar_t * _DstBuf,   size_t _MaxCount,     const wchar_t * _Format,   _locale_t _Locale, va_list _ArgList);
  int __cdecl _vsnwprintf_s_l(  wchar_t * _DstBuf,   size_t _DstSize,   size_t _MaxCount,     const wchar_t * _Format,   _locale_t _Locale, va_list _ArgList);













#pragma warning(push)
#pragma warning(disable:4141 4996 4793)
__declspec(deprecated("This function or variable may be unsafe. Consider using " "_swprintf_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details.")) __declspec(deprecated("swprintf has been changed to conform with the ISO C standard, adding an extra character count parameter. To use traditional Microsoft swprintf, set _CRT_NON_CONFORMING_SWPRINTFS."))  int __cdecl _swprintf(    wchar_t *_Dest,     const wchar_t * _Format, ...); __declspec(deprecated("This function or variable may be unsafe. Consider using " "vswprintf_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details.")) __declspec(deprecated("swprintf has been changed to conform with the ISO C standard, adding an extra character count parameter. To use traditional Microsoft swprintf, set _CRT_NON_CONFORMING_SWPRINTFS."))  int __cdecl _vswprintf(    wchar_t *_Dest,     const wchar_t * _Format, va_list _Args);
__declspec(deprecated("This function or variable may be unsafe. Consider using " "__swprintf_l_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details.")) __declspec(deprecated("swprintf has been changed to conform with the ISO C standard, adding an extra character count parameter. To use traditional Microsoft swprintf, set _CRT_NON_CONFORMING_SWPRINTFS."))  int __cdecl __swprintf_l(    wchar_t *_Dest,     const wchar_t * _Format, _locale_t _Plocinfo, ...); __declspec(deprecated("This function or variable may be unsafe. Consider using " "_vswprintf_s_l" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details.")) __declspec(deprecated("swprintf has been changed to conform with the ISO C standard, adding an extra character count parameter. To use traditional Microsoft swprintf, set _CRT_NON_CONFORMING_SWPRINTFS."))  int __cdecl __vswprintf_l(    wchar_t *_Dest,     const wchar_t * _Format, _locale_t _Plocinfo, va_list _Args);
#pragma warning(pop)















#pragma once




















#pragma warning( push )
#pragma warning( disable : 4793 4412 )
static __inline int swprintf(  wchar_t * _String, size_t _Count,     const wchar_t * _Format, ...)
{
    va_list _Arglist;
    int _Ret;
    ( _Arglist = (va_list)( &reinterpret_cast<const char &>(_Format) ) + ( (sizeof(_Format) + sizeof(int) - 1) & ~(sizeof(int) - 1) ) );
    _Ret = _vswprintf_c_l(_String, _Count, _Format, 0, _Arglist);
    ( _Arglist = (va_list)0 );
    return _Ret;
}
#pragma warning( pop )

#pragma warning( push )
#pragma warning( disable : 4412 )
static __inline int __cdecl vswprintf(  wchar_t * _String, size_t _Count,     const wchar_t * _Format, va_list _Ap)
{
    return _vswprintf_c_l(_String, _Count, _Format, 0, _Ap);
}
#pragma warning( pop )




#pragma warning( push )
#pragma warning( disable : 4793 4412 )
static __inline int _swprintf_l(  wchar_t * _String, size_t _Count,     const wchar_t * _Format, _locale_t _Plocinfo, ...)
{
    va_list _Arglist;
    int _Ret;
    ( _Arglist = (va_list)( &reinterpret_cast<const char &>(_Plocinfo) ) + ( (sizeof(_Plocinfo) + sizeof(int) - 1) & ~(sizeof(int) - 1) ) );
    _Ret = _vswprintf_c_l(_String, _Count, _Format, _Plocinfo, _Arglist);
    ( _Arglist = (va_list)0 );
    return _Ret;
}
#pragma warning( pop )

#pragma warning( push )
#pragma warning( disable : 4412 )
static __inline int __cdecl _vswprintf_l(  wchar_t * _String, size_t _Count,     const wchar_t * _Format, _locale_t _Plocinfo, va_list _Ap)
{
    return _vswprintf_c_l(_String, _Count, _Format, _Plocinfo, _Ap);
}
#pragma warning( pop )


#pragma warning( push )
#pragma warning( disable : 4996 )

#pragma warning( push )
#pragma warning( disable : 4793 4141 )
extern "C++" __declspec(deprecated("swprintf has been changed to conform with the ISO C standard, adding an extra character count parameter. To use traditional Microsoft swprintf, set _CRT_NON_CONFORMING_SWPRINTFS.")) __declspec(deprecated("This function or variable may be unsafe. Consider using " "swprintf_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details.")) __inline int swprintf(    wchar_t * _String,     const wchar_t * _Format, ...)
{
    va_list _Arglist;
    ( _Arglist = (va_list)( &reinterpret_cast<const char &>(_Format) ) + ( (sizeof(_Format) + sizeof(int) - 1) & ~(sizeof(int) - 1) ) );
    int _Ret = _vswprintf(_String, _Format, _Arglist);
    ( _Arglist = (va_list)0 );
    return _Ret;
}
#pragma warning( pop )

#pragma warning( push )
#pragma warning( disable : 4141 )
extern "C++" __declspec(deprecated("swprintf has been changed to conform with the ISO C standard, adding an extra character count parameter. To use traditional Microsoft swprintf, set _CRT_NON_CONFORMING_SWPRINTFS.")) __declspec(deprecated("This function or variable may be unsafe. Consider using " "vswprintf_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details.")) __inline int __cdecl vswprintf(    wchar_t * _String,     const wchar_t * _Format, va_list _Ap)
{
    return _vswprintf(_String, _Format, _Ap);
}
#pragma warning( pop )

#pragma warning( push )
#pragma warning( disable : 4793 4141 )
extern "C++" __declspec(deprecated("swprintf has been changed to conform with the ISO C standard, adding an extra character count parameter. To use traditional Microsoft swprintf, set _CRT_NON_CONFORMING_SWPRINTFS.")) __declspec(deprecated("This function or variable may be unsafe. Consider using " "_swprintf_s_l" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details.")) __inline int _swprintf_l(    wchar_t * _String,     const wchar_t * _Format, _locale_t _Plocinfo, ...)
{
    va_list _Arglist;
    ( _Arglist = (va_list)( &reinterpret_cast<const char &>(_Plocinfo) ) + ( (sizeof(_Plocinfo) + sizeof(int) - 1) & ~(sizeof(int) - 1) ) );
    int _Ret = __vswprintf_l(_String, _Format, _Plocinfo, _Arglist);
    ( _Arglist = (va_list)0 );
    return _Ret;
}
#pragma warning( pop )

#pragma warning( push )
#pragma warning( disable : 4141 )
extern "C++" __declspec(deprecated("swprintf has been changed to conform with the ISO C standard, adding an extra character count parameter. To use traditional Microsoft swprintf, set _CRT_NON_CONFORMING_SWPRINTFS.")) __declspec(deprecated("This function or variable may be unsafe. Consider using " "_vswprintf_s_l" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details.")) __inline int __cdecl _vswprintf_l(    wchar_t * _String,     const wchar_t * _Format, _locale_t _Plocinfo, va_list _Ap)
{
    return __vswprintf_l(_String, _Format, _Plocinfo, _Ap);
}
#pragma warning( pop )

#pragma warning( pop )























   wchar_t * __cdecl _wtempnam(  const wchar_t * _Directory,   const wchar_t * _FilePrefix);





   int __cdecl _vscwprintf(    const wchar_t * _Format, va_list _ArgList);
   int __cdecl _vscwprintf_l(    const wchar_t * _Format,   _locale_t _Locale, va_list _ArgList);
  __declspec(deprecated("This function or variable may be unsafe. Consider using " "fwscanf_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  int __cdecl fwscanf(  FILE * _File,     const wchar_t * _Format, ...);
 __declspec(deprecated("This function or variable may be unsafe. Consider using " "_fwscanf_s_l" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  int __cdecl _fwscanf_l(  FILE * _File,     const wchar_t * _Format,   _locale_t _Locale, ...);
#pragma warning(push)
#pragma warning(disable:6530)

  int __cdecl fwscanf_s(  FILE * _File,     const wchar_t * _Format, ...);

  int __cdecl _fwscanf_s_l(  FILE * _File,     const wchar_t * _Format,   _locale_t _Locale, ...);
  __declspec(deprecated("This function or variable may be unsafe. Consider using " "swscanf_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  int __cdecl swscanf(  const wchar_t * _Src,     const wchar_t * _Format, ...);
 __declspec(deprecated("This function or variable may be unsafe. Consider using " "_swscanf_s_l" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  int __cdecl _swscanf_l(  const wchar_t * _Src,     const wchar_t * _Format,   _locale_t _Locale, ...);

  int __cdecl swscanf_s(  const wchar_t *_Src,     const wchar_t * _Format, ...);

  int __cdecl _swscanf_s_l(  const wchar_t * _Src,     const wchar_t * _Format,   _locale_t _Locale, ...);
 __declspec(deprecated("This function or variable may be unsafe. Consider using " "_snwscanf_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  int __cdecl _snwscanf(    const wchar_t * _Src,   size_t _MaxCount,     const wchar_t * _Format, ...);
 __declspec(deprecated("This function or variable may be unsafe. Consider using " "_snwscanf_s_l" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  int __cdecl _snwscanf_l(    const wchar_t * _Src,   size_t _MaxCount,     const wchar_t * _Format,   _locale_t _Locale, ...);
  int __cdecl _snwscanf_s(    const wchar_t * _Src,   size_t _MaxCount,     const wchar_t * _Format, ...);
  int __cdecl _snwscanf_s_l(    const wchar_t * _Src,   size_t _MaxCount,     const wchar_t * _Format,   _locale_t _Locale, ...);
  __declspec(deprecated("This function or variable may be unsafe. Consider using " "wscanf_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  int __cdecl wscanf(    const wchar_t * _Format, ...);
 __declspec(deprecated("This function or variable may be unsafe. Consider using " "_wscanf_s_l" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  int __cdecl _wscanf_l(    const wchar_t * _Format,   _locale_t _Locale, ...);

  int __cdecl wscanf_s(    const wchar_t * _Format, ...);

  int __cdecl _wscanf_s_l(    const wchar_t * _Format,   _locale_t _Locale, ...);
#pragma warning(pop)

   FILE * __cdecl _wfdopen(  int _FileHandle ,   const wchar_t * _Mode);
  __declspec(deprecated("This function or variable may be unsafe. Consider using " "_wfopen_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  FILE * __cdecl _wfopen(  const wchar_t * _Filename,   const wchar_t * _Mode);
  errno_t __cdecl _wfopen_s(  FILE ** _File,   const wchar_t * _Filename,   const wchar_t * _Mode);
  __declspec(deprecated("This function or variable may be unsafe. Consider using " "_wfreopen_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  FILE * __cdecl _wfreopen(  const wchar_t * _Filename,   const wchar_t * _Mode,   FILE * _OldFile);
  errno_t __cdecl _wfreopen_s(  FILE ** _File,   const wchar_t * _Filename,   const wchar_t * _Mode,   FILE * _OldFile);



 void __cdecl _wperror(  const wchar_t * _ErrMsg);


   FILE * __cdecl _wpopen(  const wchar_t *_Command,   const wchar_t * _Mode);

 int __cdecl _wremove(  const wchar_t * _Filename);
  errno_t __cdecl _wtmpnam_s(  wchar_t * _DstBuf,   size_t _SizeInWords);
extern "C++" { template <size_t _Size> inline errno_t __cdecl _wtmpnam_s(  wchar_t (&_Buffer)[_Size]) throw() { return _wtmpnam_s(_Buffer, _Size); } }
__declspec(deprecated("This function or variable may be unsafe. Consider using " "_wtmpnam_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  wchar_t * __cdecl _wtmpnam(  wchar_t *_Buffer);

  wint_t __cdecl _fgetwc_nolock(  FILE * _File);
  wint_t __cdecl _fputwc_nolock(  wchar_t _Ch,   FILE * _File);
  wint_t __cdecl _ungetwc_nolock(  wint_t _Ch,   FILE * _File);











inline   wint_t __cdecl getwchar()
        {return (fgetwc((&__iob_func()[0]))); }   
inline  wint_t __cdecl putwchar(  wchar_t _C)
        {return (fputwc(_C, (&__iob_func()[1]))); }       













































 void __cdecl _lock_file(  FILE * _File);
 void __cdecl _unlock_file(  FILE * _File);

  int __cdecl _fclose_nolock(  FILE * _File);
  int __cdecl _fflush_nolock(  FILE * _File);
  size_t __cdecl _fread_nolock(  void * _DstBuf,   size_t _ElementSize,   size_t _Count,   FILE * _File);
  size_t __cdecl _fread_nolock_s(  void * _DstBuf,   size_t _DstSize,   size_t _ElementSize,   size_t _Count,   FILE * _File);
  int __cdecl _fseek_nolock(  FILE * _File,   long _Offset,   int _Origin);
   long __cdecl _ftell_nolock(  FILE * _File);
  int __cdecl _fseeki64_nolock(  FILE * _File,   __int64 _Offset,   int _Origin);
   __int64 __cdecl _ftelli64_nolock(  FILE * _File);
  size_t __cdecl _fwrite_nolock(  const void * _DstBuf,   size_t _Size,   size_t _Count,   FILE * _File);
  int __cdecl _ungetc_nolock(  int _Ch,   FILE * _File);


























__declspec(deprecated("The POSIX name for this item is deprecated. Instead, use the ISO C++ conformant name: " "_tempnam" ". See online help for details."))  char * __cdecl tempnam(  const char * _Directory,   const char * _FilePrefix);





 __declspec(deprecated("The POSIX name for this item is deprecated. Instead, use the ISO C++ conformant name: " "_fcloseall" ". See online help for details."))  int __cdecl fcloseall(void);
  __declspec(deprecated("The POSIX name for this item is deprecated. Instead, use the ISO C++ conformant name: " "_fdopen" ". See online help for details."))  FILE * __cdecl fdopen(  int _FileHandle,   const char * _Format);
 __declspec(deprecated("The POSIX name for this item is deprecated. Instead, use the ISO C++ conformant name: " "_fgetchar" ". See online help for details."))  int __cdecl fgetchar(void);
  __declspec(deprecated("The POSIX name for this item is deprecated. Instead, use the ISO C++ conformant name: " "_fileno" ". See online help for details."))  int __cdecl fileno(  FILE * _File);
 __declspec(deprecated("The POSIX name for this item is deprecated. Instead, use the ISO C++ conformant name: " "_flushall" ". See online help for details."))  int __cdecl flushall(void);
 __declspec(deprecated("The POSIX name for this item is deprecated. Instead, use the ISO C++ conformant name: " "_fputchar" ". See online help for details."))  int __cdecl fputchar(  int _Ch);
  __declspec(deprecated("The POSIX name for this item is deprecated. Instead, use the ISO C++ conformant name: " "_getw" ". See online help for details."))  int __cdecl getw(  FILE * _File);
 __declspec(deprecated("The POSIX name for this item is deprecated. Instead, use the ISO C++ conformant name: " "_putw" ". See online help for details."))  int __cdecl putw(  int _Ch,   FILE * _File);
  __declspec(deprecated("The POSIX name for this item is deprecated. Instead, use the ISO C++ conformant name: " "_rmtmp" ". See online help for details."))  int __cdecl rmtmp(void);




}


#pragma pack(pop)






















 




static const float RC_PI = 3.14159265f;



enum rcLogCategory
{
	RC_LOG_PROGRESS = 1,	
	RC_LOG_WARNING,			
	RC_LOG_ERROR,			
};



enum rcTimerLabel
{
	
	RC_TIMER_TOTAL,
	
	RC_TIMER_TEMP,
	
	RC_TIMER_RASTERIZE_TRIANGLES,
	
	RC_TIMER_BUILD_COMPACTHEIGHTFIELD,
	
	RC_TIMER_BUILD_CONTOURS,
	
	RC_TIMER_BUILD_CONTOURS_TRACE,
	
	RC_TIMER_BUILD_CONTOURS_SIMPLIFY,
	
	RC_TIMER_FILTER_BORDER,
	
	RC_TIMER_FILTER_WALKABLE,
	
	RC_TIMER_MEDIAN_AREA,
	
	RC_TIMER_FILTER_LOW_OBSTACLES,
	
	RC_TIMER_BUILD_POLYMESH,
	
	RC_TIMER_MERGE_POLYMESH,
	
	RC_TIMER_ERODE_AREA,
	
	RC_TIMER_MARK_BOX_AREA,
	
	RC_TIMER_MARK_CYLINDER_AREA,
	
	RC_TIMER_MARK_CONVEXPOLY_AREA,
	
	RC_TIMER_BUILD_DISTANCEFIELD,
	
	RC_TIMER_BUILD_DISTANCEFIELD_DIST,
	
	RC_TIMER_BUILD_DISTANCEFIELD_BLUR,
	
	RC_TIMER_BUILD_REGIONS,
	
	RC_TIMER_BUILD_REGIONS_WATERSHED,
	
	RC_TIMER_BUILD_REGIONS_EXPAND,
	
	RC_TIMER_BUILD_REGIONS_FLOOD,
	
	RC_TIMER_BUILD_REGIONS_FILTER,
	
	RC_TIMER_BUILD_LAYERS, 
	
	RC_TIMER_BUILD_POLYMESHDETAIL,
	
	RC_TIMER_MERGE_POLYMESHDETAIL,
	
	RC_MAX_TIMERS
};




class rcContext
{
public:

	
	
	inline rcContext(bool state = true) : m_logEnabled(state), m_timerEnabled(state) {}
	virtual ~rcContext() {}

	
	
	inline void enableLog(bool state) { m_logEnabled = state; }

	
	inline void resetLog() { if (m_logEnabled) doResetLog(); }

	
	
	
	void log(const rcLogCategory category, const char* format, ...);

	
	
	inline void enableTimer(bool state) { m_timerEnabled = state; }

	
	inline void resetTimers() { if (m_timerEnabled) doResetTimers(); }

	
	
	inline void startTimer(const rcTimerLabel label) { if (m_timerEnabled) doStartTimer(label); }

	
	
	inline void stopTimer(const rcTimerLabel label) { if (m_timerEnabled) doStopTimer(label); }

	
	
	
	inline int getAccumulatedTime(const rcTimerLabel label) const { return m_timerEnabled ? doGetAccumulatedTime(label) : -1; }

protected:

	
	virtual void doResetLog() {}

	
	
	
	
	virtual void doLog(const rcLogCategory , const char* , const int ) {}

	
	virtual void doResetTimers() {}

	
	
	virtual void doStartTimer(const rcTimerLabel ) {}

	
	
	virtual void doStopTimer(const rcTimerLabel ) {}

	
	
	
	virtual int doGetAccumulatedTime(const rcTimerLabel ) const { return -1; }
	
	
	bool m_logEnabled;

	
	bool m_timerEnabled;
};



struct rcConfig
{
	
	int width;

	
	int height;
	
	
	int tileSize;
	
	
	int borderSize;

	
	float cs;

	
	float ch;

	
	float bmin[3]; 

	
	float bmax[3];

	
	float walkableSlopeAngle;

	
	
	int walkableHeight;
	
	
	int walkableClimb;
	
	
	
	int walkableRadius;
	
	
	int maxEdgeLen;
	
	
	
	float maxSimplificationError;
	
	
	int minRegionArea;
	
	
	
	int mergeRegionArea;
	
	
	
	int maxVertsPerPoly;
	
	
	
	float detailSampleDist;
	
	
	
	float detailSampleMaxError;
};


static const int RC_SPAN_HEIGHT_BITS = 13;

static const int RC_SPAN_MAX_HEIGHT = (1<<RC_SPAN_HEIGHT_BITS)-1;



static const int RC_SPANS_PER_POOL = 2048;



struct rcSpan
{
	unsigned int smin : 13;			
	unsigned int smax : 13;			
	unsigned int area : 6;			
	rcSpan* next;					
};



struct rcSpanPool
{
	rcSpanPool* next;					
	rcSpan items[RC_SPANS_PER_POOL];	
};



struct rcHeightfield
{
	int width;			
	int height;			
	float bmin[3];  	
	float bmax[3];		
	float cs;			
	float ch;			
	rcSpan** spans;		
	rcSpanPool* pools;	
	rcSpan* freelist;	
};


struct rcCompactCell
{
	unsigned int index : 24;	
	unsigned int count : 8;		
};


struct rcCompactSpan
{
	unsigned short y;			
	unsigned short reg;			
	unsigned int con : 24;		
	unsigned int h : 8;			
};



struct rcCompactHeightfield
{
	int width;					
	int height;					
	int spanCount;				
	int walkableHeight;			
	int walkableClimb;			
	int borderSize;				
	unsigned short maxDistance;	
	unsigned short maxRegions;	
	float bmin[3];				
	float bmax[3];				
	float cs;					
	float ch;					
	rcCompactCell* cells;		
	rcCompactSpan* spans;		
	unsigned short* dist;		
	unsigned char* areas;		
};



struct rcHeightfieldLayer
{
	float bmin[3];				
	float bmax[3];				
	float cs;					
	float ch;					
	int width;					
	int height;					
	int minx;					
	int maxx;					
	int miny;					
	int maxy;					
	int hmin;					
	int hmax;					
	unsigned char* heights;		
	unsigned char* areas;		
	unsigned char* cons;		
};




struct rcHeightfieldLayerSet
{
	rcHeightfieldLayer* layers;			
	int nlayers;						
};


struct rcContour
{
	int* verts;			
	int nverts;			
	int* rverts;		
	int nrverts;		
	unsigned short reg;	
	unsigned char area;	
};



struct rcContourSet
{
	rcContour* conts;	
	int nconts;			
	float bmin[3];  	
	float bmax[3];		
	float cs;			
	float ch;			
	int width;			
	int height;			
	int borderSize;		
};



struct rcPolyMesh
{
	unsigned short* verts;	
	unsigned short* polys;	
	unsigned short* regs;	
	unsigned short* flags;	
	unsigned char* areas;	
	int nverts;				
	int npolys;				
	int maxpolys;			
	int nvp;				
	float bmin[3];			
	float bmax[3];			
	float cs;				
	float ch;				
	int borderSize;			
};




struct rcPolyMeshDetail
{
	unsigned int* meshes;	
	float* verts;			
	unsigned char* tris;	
	int nmeshes;			
	int nverts;				
	int ntris;				
};










rcHeightfield* rcAllocHeightfield();





void rcFreeHeightField(rcHeightfield* hf);





rcCompactHeightfield* rcAllocCompactHeightfield();





void rcFreeCompactHeightfield(rcCompactHeightfield* chf);





rcHeightfieldLayerSet* rcAllocHeightfieldLayerSet();





void rcFreeHeightfieldLayerSet(rcHeightfieldLayerSet* lset);





rcContourSet* rcAllocContourSet();





void rcFreeContourSet(rcContourSet* cset);





rcPolyMesh* rcAllocPolyMesh();





void rcFreePolyMesh(rcPolyMesh* pmesh);





rcPolyMeshDetail* rcAllocPolyMeshDetail();





void rcFreePolyMeshDetail(rcPolyMeshDetail* dmesh);








static const unsigned short RC_BORDER_REG = 0x8000;








static const int RC_BORDER_VERTEX = 0x10000;






static const int RC_AREA_BORDER = 0x20000;



enum rcBuildContoursFlags
{
	RC_CONTOUR_TESS_WALL_EDGES = 0x01,	
	RC_CONTOUR_TESS_AREA_EDGES = 0x02,	
};





static const int RC_CONTOUR_REG_MASK = 0xffff;




static const unsigned short RC_MESH_NULL_IDX = 0xffff;




static const unsigned char RC_NULL_AREA = 0;




static const unsigned char RC_WALKABLE_AREA = 63;



static const int RC_NOT_CONNECTED = 0x3f;







template<class T> inline void rcSwap(T& a, T& b) { T t = a; a = b; b = t; }





template<class T> inline T rcMin(T a, T b) { return a < b ? a : b; }





template<class T> inline T rcMax(T a, T b) { return a > b ? a : b; }




template<class T> inline T rcAbs(T a) { return a < 0 ? -a : a; }




template<class T> inline T rcSqr(T a) { return a*a; }






template<class T> inline T rcClamp(T v, T mn, T mx) { return v < mn ? mn : (v > mx ? mx : v); }




float rcSqrt(float x);









inline void rcVcross(float* dest, const float* v1, const float* v2)
{
	dest[0] = v1[1]*v2[2] - v1[2]*v2[1];
	dest[1] = v1[2]*v2[0] - v1[0]*v2[2];
	dest[2] = v1[0]*v2[1] - v1[1]*v2[0];
}





inline float rcVdot(const float* v1, const float* v2)
{
	return v1[0]*v2[0] + v1[1]*v2[1] + v1[2]*v2[2];
}






inline void rcVmad(float* dest, const float* v1, const float* v2, const float s)
{
	dest[0] = v1[0]+v2[0]*s;
	dest[1] = v1[1]+v2[1]*s;
	dest[2] = v1[2]+v2[2]*s;
}





inline void rcVadd(float* dest, const float* v1, const float* v2)
{
	dest[0] = v1[0]+v2[0];
	dest[1] = v1[1]+v2[1];
	dest[2] = v1[2]+v2[2];
}





inline void rcVsub(float* dest, const float* v1, const float* v2)
{
	dest[0] = v1[0]-v2[0];
	dest[1] = v1[1]-v2[1];
	dest[2] = v1[2]-v2[2];
}




inline void rcVmin(float* mn, const float* v)
{
	mn[0] = rcMin(mn[0], v[0]);
	mn[1] = rcMin(mn[1], v[1]);
	mn[2] = rcMin(mn[2], v[2]);
}




inline void rcVmax(float* mx, const float* v)
{
	mx[0] = rcMax(mx[0], v[0]);
	mx[1] = rcMax(mx[1], v[1]);
	mx[2] = rcMax(mx[2], v[2]);
}




inline void rcVcopy(float* dest, const float* v)
{
	dest[0] = v[0];
	dest[1] = v[1];
	dest[2] = v[2];
}





inline float rcVdist(const float* v1, const float* v2)
{
	float dx = v2[0] - v1[0];
	float dy = v2[1] - v1[1];
	float dz = v2[2] - v1[2];
	return rcSqrt(dx*dx + dy*dy + dz*dz);
}





inline float rcVdistSqr(const float* v1, const float* v2)
{
	float dx = v2[0] - v1[0];
	float dy = v2[1] - v1[1];
	float dz = v2[2] - v1[2];
	return dx*dx + dy*dy + dz*dz;
}



inline void rcVnormalize(float* v)
{
	float d = 1.0f / rcSqrt(rcSqr(v[0]) + rcSqr(v[1]) + rcSqr(v[2]));
	v[0] *= d;
	v[1] *= d;
	v[2] *= d;
}












void rcCalcBounds(const float* verts, int nv, float* bmin, float* bmax);








void rcCalcGridSize(const float* bmin, const float* bmax, float cs, int* w, int* h);











bool rcCreateHeightfield(rcContext* ctx, rcHeightfield& hf, int width, int height,
						 const float* bmin, const float* bmax,
						 float cs, float ch);












void rcMarkWalkableTriangles(rcContext* ctx, const float walkableSlopeAngle, const float* verts, int nv,
							 const int* tris, int nt, unsigned char* areas); 











void rcClearUnwalkableTriangles(rcContext* ctx, const float walkableSlopeAngle, const float* verts, int nv,
								const int* tris, int nt, unsigned char* areas); 













void rcAddSpan(rcContext* ctx, rcHeightfield& hf, const int x, const int y,
			   const unsigned short smin, const unsigned short smax,
			   const unsigned char area, const int flagMergeThr);











void rcRasterizeTriangle(rcContext* ctx, const float* v0, const float* v1, const float* v2,
						 const unsigned char area, rcHeightfield& solid,
						 const int flagMergeThr = 1);












void rcRasterizeTriangles(rcContext* ctx, const float* verts, const int nv,
						  const int* tris, const unsigned char* areas, const int nt,
						  rcHeightfield& solid, const int flagMergeThr = 1);












void rcRasterizeTriangles(rcContext* ctx, const float* verts, const int nv,
						  const unsigned short* tris, const unsigned char* areas, const int nt,
						  rcHeightfield& solid, const int flagMergeThr = 1);










void rcRasterizeTriangles(rcContext* ctx, const float* verts, const unsigned char* areas, const int nt,
						  rcHeightfield& solid, const int flagMergeThr = 1);







void rcFilterLowHangingWalkableObstacles(rcContext* ctx, const int walkableClimb, rcHeightfield& solid);









void rcFilterLedgeSpans(rcContext* ctx, const int walkableHeight,
						const int walkableClimb, rcHeightfield& solid);







void rcFilterWalkableLowHeightSpans(rcContext* ctx, int walkableHeight, rcHeightfield& solid);






int rcGetHeightFieldSpanCount(rcContext* ctx, rcHeightfield& hf);
















bool rcBuildCompactHeightfield(rcContext* ctx, const int walkableHeight, const int walkableClimb,
							   rcHeightfield& hf, rcCompactHeightfield& chf);







bool rcErodeWalkableArea(rcContext* ctx, int radius, rcCompactHeightfield& chf);






bool rcMedianFilterWalkableArea(rcContext* ctx, rcCompactHeightfield& chf);








void rcMarkBoxArea(rcContext* ctx, const float* bmin, const float* bmax, unsigned char areaId,
				   rcCompactHeightfield& chf);










void rcMarkConvexPolyArea(rcContext* ctx, const float* verts, const int nverts,
						  const float hmin, const float hmax, unsigned char areaId,
						  rcCompactHeightfield& chf);








int rcOffsetPoly(const float* verts, const int nverts, const float offset,
				 float* outVerts, const int maxOutVerts);









void rcMarkCylinderArea(rcContext* ctx, const float* pos,
						const float r, const float h, unsigned char areaId,
						rcCompactHeightfield& chf);






bool rcBuildDistanceField(rcContext* ctx, rcCompactHeightfield& chf);












bool rcBuildRegions(rcContext* ctx, rcCompactHeightfield& chf,
					const int borderSize, const int minRegionArea, const int mergeRegionArea);












bool rcBuildRegionsMonotone(rcContext* ctx, rcCompactHeightfield& chf,
							const int borderSize, const int minRegionArea, const int mergeRegionArea);






inline void rcSetCon(rcCompactSpan& s, int dir, int i)
{
	const unsigned int shift = (unsigned int)dir*6;
	unsigned int con = s.con;
	s.con = (con & ~(0x3f << shift)) | (((unsigned int)i & 0x3f) << shift);
}






inline int rcGetCon(const rcCompactSpan& s, int dir)
{
	const unsigned int shift = (unsigned int)dir*6;
	return (s.con >> shift) & 0x3f;
}





inline int rcGetDirOffsetX(int dir)
{
	const int offset[4] = { -1, 0, 1, 0, };
	return offset[dir&0x03];
}





inline int rcGetDirOffsetY(int dir)
{
	const int offset[4] = { 0, 1, 0, -1 };
	return offset[dir&0x03];
}
















bool rcBuildHeightfieldLayers(rcContext* ctx, rcCompactHeightfield& chf, 
							  const int borderSize, const int walkableHeight,
							  rcHeightfieldLayerSet& lset);












bool rcBuildContours(rcContext* ctx, rcCompactHeightfield& chf,
					 const float maxError, const int maxEdgeLen,
					 rcContourSet& cset, const int flags = RC_CONTOUR_TESS_WALL_EDGES);









bool rcBuildPolyMesh(rcContext* ctx, rcContourSet& cset, const int nvp, rcPolyMesh& mesh);








bool rcMergePolyMeshes(rcContext* ctx, rcPolyMesh** meshes, const int nmeshes, rcPolyMesh& mesh);











bool rcBuildPolyMeshDetail(rcContext* ctx, const rcPolyMesh& mesh, const rcCompactHeightfield& chf,
						   const float sampleDist, const float sampleMaxError,
						   rcPolyMeshDetail& dmesh);







bool rcCopyPolyMesh(rcContext* ctx, const rcPolyMesh& src, rcPolyMesh& dst);








bool rcMergePolyMeshDetails(rcContext* ctx, rcPolyMeshDetail** meshes, const int nmeshes, rcPolyMeshDetail& mesh);

















































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































extern "C" {


 void __cdecl _wassert(  const wchar_t * _Message,   const wchar_t *_File,   unsigned _Line);


}























void rcFilterLowHangingWalkableObstacles(rcContext* ctx, const int walkableClimb, rcHeightfield& solid)
{
	(void)( (!!(ctx)) || (_wassert(L"ctx", L"e:\\work\\fwcyclehero\\server\\src\\lib\\navigation\\recastfilter.cpp", 38), 0) );

	ctx->startTimer(RC_TIMER_FILTER_LOW_OBSTACLES);
	
	const int w = solid.width;
	const int h = solid.height;
	
	for (int y = 0; y < h; ++y)
	{
		for (int x = 0; x < w; ++x)
		{
			rcSpan* ps = 0;
			bool previousWalkable = false;
			unsigned char previousArea = RC_NULL_AREA;
			
			for (rcSpan* s = solid.spans[x + y*w]; s; ps = s, s = s->next)
			{
				const bool walkable = s->area != RC_NULL_AREA;
				
				
				if (!walkable && previousWalkable)
				{
					if (rcAbs((int)s->smax - (int)ps->smax) <= walkableClimb)
						s->area = previousArea;
				}
				
				
				previousWalkable = walkable;
				previousArea = s->area;
			}
		}
	}

	ctx->stopTimer(RC_TIMER_FILTER_LOW_OBSTACLES);
}











void rcFilterLedgeSpans(rcContext* ctx, const int walkableHeight, const int walkableClimb,
						rcHeightfield& solid)
{
	(void)( (!!(ctx)) || (_wassert(L"ctx", L"e:\\work\\fwcyclehero\\server\\src\\lib\\navigation\\recastfilter.cpp", 87), 0) );
	
	ctx->startTimer(RC_TIMER_FILTER_BORDER);

	const int w = solid.width;
	const int h = solid.height;
	const int MAX_HEIGHT = 0xffff;
	
	
	for (int y = 0; y < h; ++y)
	{
		for (int x = 0; x < w; ++x)
		{
			for (rcSpan* s = solid.spans[x + y*w]; s; s = s->next)
			{
				
				if (s->area == RC_NULL_AREA)
					continue;
				
				const int bot = (int)(s->smax);
				const int top = s->next ? (int)(s->next->smin) : MAX_HEIGHT;
				
				
				int minh = MAX_HEIGHT;

				
				int asmin = s->smax;
				int asmax = s->smax;

				for (int dir = 0; dir < 4; ++dir)
				{
					int dx = x + rcGetDirOffsetX(dir);
					int dy = y + rcGetDirOffsetY(dir);
					
					if (dx < 0 || dy < 0 || dx >= w || dy >= h)
					{
						minh = rcMin(minh, -walkableClimb - bot);
						continue;
					}

					
					rcSpan* ns = solid.spans[dx + dy*w];
					int nbot = -walkableClimb;
					int ntop = ns ? (int)ns->smin : MAX_HEIGHT;
					
					if (rcMin(top,ntop) - rcMax(bot,nbot) > walkableHeight)
						minh = rcMin(minh, nbot - bot);
					
					
					for (ns = solid.spans[dx + dy*w]; ns; ns = ns->next)
					{
						nbot = (int)ns->smax;
						ntop = ns->next ? (int)ns->next->smin : MAX_HEIGHT;
						
						if (rcMin(top,ntop) - rcMax(bot,nbot) > walkableHeight)
						{
							minh = rcMin(minh, nbot - bot);
						
							
							if (rcAbs(nbot - bot) <= walkableClimb)
							{
								if (nbot < asmin) asmin = nbot;
								if (nbot > asmax) asmax = nbot;
							}
							
						}
					}
				}
				
				
				
				if (minh < -walkableClimb)
					s->area = RC_NULL_AREA;
					
				
				
				if ((asmax - asmin) > walkableClimb)
				{
					s->area = RC_NULL_AREA;
				}
			}
		}
	}
	
	ctx->stopTimer(RC_TIMER_FILTER_BORDER);
}	







void rcFilterWalkableLowHeightSpans(rcContext* ctx, int walkableHeight, rcHeightfield& solid)
{
	(void)( (!!(ctx)) || (_wassert(L"ctx", L"e:\\work\\fwcyclehero\\server\\src\\lib\\navigation\\recastfilter.cpp", 182), 0) );
	
	ctx->startTimer(RC_TIMER_FILTER_WALKABLE);
	
	const int w = solid.width;
	const int h = solid.height;
	const int MAX_HEIGHT = 0xffff;
	
	
	
	for (int y = 0; y < h; ++y)
	{
		for (int x = 0; x < w; ++x)
		{
			for (rcSpan* s = solid.spans[x + y*w]; s; s = s->next)
			{
				const int bot = (int)(s->smax);
				const int top = s->next ? (int)(s->next->smin) : MAX_HEIGHT;
				if ((top - bot) <= walkableHeight)
					s->area = RC_NULL_AREA;
			}
		}
	}
	
	ctx->stopTimer(RC_TIMER_FILTER_WALKABLE);
}
