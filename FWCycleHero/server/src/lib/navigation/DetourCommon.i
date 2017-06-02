





















































































































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





















































float dtSqrt(float x)
{
	return sqrtf(x);
}

void dtClosestPtPointTriangle(float* closest, const float* p,
							  const float* a, const float* b, const float* c)
{
	
	float ab[3], ac[3], ap[3];
	dtVsub(ab, b, a);
	dtVsub(ac, c, a);
	dtVsub(ap, p, a);
	float d1 = dtVdot(ab, ap);
	float d2 = dtVdot(ac, ap);
	if (d1 <= 0.0f && d2 <= 0.0f)
	{
		
		dtVcopy(closest, a);
		return;
	}
	
	
	float bp[3];
	dtVsub(bp, p, b);
	float d3 = dtVdot(ab, bp);
	float d4 = dtVdot(ac, bp);
	if (d3 >= 0.0f && d4 <= d3)
	{
		
		dtVcopy(closest, b);
		return;
	}
	
	
	float vc = d1*d4 - d3*d2;
	if (vc <= 0.0f && d1 >= 0.0f && d3 <= 0.0f)
	{
		
		float v = d1 / (d1 - d3);
		closest[0] = a[0] + v * ab[0];
		closest[1] = a[1] + v * ab[1];
		closest[2] = a[2] + v * ab[2];
		return;
	}
	
	
	float cp[3];
	dtVsub(cp, p, c);
	float d5 = dtVdot(ab, cp);
	float d6 = dtVdot(ac, cp);
	if (d6 >= 0.0f && d5 <= d6)
	{
		
		dtVcopy(closest, c);
		return;
	}
	
	
	float vb = d5*d2 - d1*d6;
	if (vb <= 0.0f && d2 >= 0.0f && d6 <= 0.0f)
	{
		
		float w = d2 / (d2 - d6);
		closest[0] = a[0] + w * ac[0];
		closest[1] = a[1] + w * ac[1];
		closest[2] = a[2] + w * ac[2];
		return;
	}
	
	
	float va = d3*d6 - d5*d4;
	if (va <= 0.0f && (d4 - d3) >= 0.0f && (d5 - d6) >= 0.0f)
	{
		
		float w = (d4 - d3) / ((d4 - d3) + (d5 - d6));
		closest[0] = b[0] + w * (c[0] - b[0]);
		closest[1] = b[1] + w * (c[1] - b[1]);
		closest[2] = b[2] + w * (c[2] - b[2]);
		return;
	}
	
	
	float denom = 1.0f / (va + vb + vc);
	float v = vb * denom;
	float w = vc * denom;
	closest[0] = a[0] + ab[0] * v + ac[0] * w;
	closest[1] = a[1] + ab[1] * v + ac[1] * w;
	closest[2] = a[2] + ab[2] * v + ac[2] * w;
}

bool dtIntersectSegmentPoly2D(const float* p0, const float* p1,
							  const float* verts, int nverts,
							  float& tmin, float& tmax,
							  int& segMin, int& segMax)
{
	static const float EPS = 0.00000001f;
	
	tmin = 0;
	tmax = 1;
	segMin = -1;
	segMax = -1;
	
	float dir[3];
	dtVsub(dir, p1, p0);
	
	for (int i = 0, j = nverts-1; i < nverts; j=i++)
	{
		float edge[3], diff[3];
		dtVsub(edge, &verts[i*3], &verts[j*3]);
		dtVsub(diff, p0, &verts[j*3]);
		const float n = dtVperp2D(edge, diff);
		const float d = dtVperp2D(dir, edge);
		if (fabsf(d) < EPS)
		{
			
			if (n < 0)
				return false;
			else
				continue;
		}
		const float t = n / d;
		if (d < 0)
		{
			
			if (t > tmin)
			{
				tmin = t;
				segMin = j;
				
				if (tmin > tmax)
					return false;
			}
		}
		else
		{
			
			if (t < tmax)
			{
				tmax = t;
				segMax = j;
				
				if (tmax < tmin)
					return false;
			}
		}
	}
	
	return true;
}

float dtDistancePtSegSqr2D(const float* pt, const float* p, const float* q, float& t)
{
	float pqx = q[0] - p[0];
	float pqz = q[2] - p[2];
	float dx = pt[0] - p[0];
	float dz = pt[2] - p[2];
	float d = pqx*pqx + pqz*pqz;
	t = pqx*dx + pqz*dz;
	if (d > 0) t /= d;
	if (t < 0) t = 0;
	else if (t > 1) t = 1;
	dx = p[0] + t*pqx - pt[0];
	dz = p[2] + t*pqz - pt[2];
	return dx*dx + dz*dz;
}

void dtCalcPolyCenter(float* tc, const unsigned short* idx, int nidx, const float* verts)
{
	tc[0] = 0.0f;
	tc[1] = 0.0f;
	tc[2] = 0.0f;
	for (int j = 0; j < nidx; ++j)
	{
		const float* v = &verts[idx[j]*3];
		tc[0] += v[0];
		tc[1] += v[1];
		tc[2] += v[2];
	}
	const float s = 1.0f / nidx;
	tc[0] *= s;
	tc[1] *= s;
	tc[2] *= s;
}

bool dtClosestHeightPointTriangle(const float* p, const float* a, const float* b, const float* c, float& h)
{
	float v0[3], v1[3], v2[3];
	dtVsub(v0, c,a);
	dtVsub(v1, b,a);
	dtVsub(v2, p,a);
	
	const float dot00 = dtVdot2D(v0, v0);
	const float dot01 = dtVdot2D(v0, v1);
	const float dot02 = dtVdot2D(v0, v2);
	const float dot11 = dtVdot2D(v1, v1);
	const float dot12 = dtVdot2D(v1, v2);
	
	
	const float invDenom = 1.0f / (dot00 * dot11 - dot01 * dot01);
	const float u = (dot11 * dot02 - dot01 * dot12) * invDenom;
	const float v = (dot00 * dot12 - dot01 * dot02) * invDenom;

	
	
	static const float EPS = 1e-4f;
	
	
	if (u >= -EPS && v >= -EPS && (u+v) <= 1+EPS)
	{
		h = a[1] + v0[1]*u + v1[1]*v;
		return true;
	}
	
	return false;
}




bool dtPointInPolygon(const float* pt, const float* verts, const int nverts)
{
	
	int i, j;
	bool c = false;
	for (i = 0, j = nverts-1; i < nverts; j = i++)
	{
		const float* vi = &verts[i*3];
		const float* vj = &verts[j*3];
		if (((vi[2] > pt[2]) != (vj[2] > pt[2])) &&
			(pt[0] < (vj[0]-vi[0]) * (pt[2]-vi[2]) / (vj[2]-vi[2]) + vi[0]) )
			c = !c;
	}
	return c;
}

bool dtDistancePtPolyEdgesSqr(const float* pt, const float* verts, const int nverts,
							  float* ed, float* et)
{
	
	int i, j;
	bool c = false;
	for (i = 0, j = nverts-1; i < nverts; j = i++)
	{
		const float* vi = &verts[i*3];
		const float* vj = &verts[j*3];
		if (((vi[2] > pt[2]) != (vj[2] > pt[2])) &&
			(pt[0] < (vj[0]-vi[0]) * (pt[2]-vi[2]) / (vj[2]-vi[2]) + vi[0]) )
			c = !c;
		ed[j] = dtDistancePtSegSqr2D(pt, vj, vi, et[j]);
	}
	return c;
}

static void projectPoly(const float* axis, const float* poly, const int npoly,
						float& rmin, float& rmax)
{
	rmin = rmax = dtVdot2D(axis, &poly[0]);
	for (int i = 1; i < npoly; ++i)
	{
		const float d = dtVdot2D(axis, &poly[i*3]);
		rmin = dtMin(rmin, d);
		rmax = dtMax(rmax, d);
	}
}

inline bool overlapRange(const float amin, const float amax,
						 const float bmin, const float bmax,
						 const float eps)
{
	return ((amin+eps) > bmax || (amax-eps) < bmin) ? false : true;
}




bool dtOverlapPolyPoly2D(const float* polya, const int npolya,
						 const float* polyb, const int npolyb)
{
	const float eps = 1e-4f;
	
	for (int i = 0, j = npolya-1; i < npolya; j=i++)
	{
		const float* va = &polya[j*3];
		const float* vb = &polya[i*3];
		const float n[3] = { vb[2]-va[2], 0, -(vb[0]-va[0]) };
		float amin,amax,bmin,bmax;
		projectPoly(n, polya, npolya, amin,amax);
		projectPoly(n, polyb, npolyb, bmin,bmax);
		if (!overlapRange(amin,amax, bmin,bmax, eps))
		{
			
			return false;
		}
	}
	for (int i = 0, j = npolyb-1; i < npolyb; j=i++)
	{
		const float* va = &polyb[j*3];
		const float* vb = &polyb[i*3];
		const float n[3] = { vb[2]-va[2], 0, -(vb[0]-va[0]) };
		float amin,amax,bmin,bmax;
		projectPoly(n, polya, npolya, amin,amax);
		projectPoly(n, polyb, npolyb, bmin,bmax);
		if (!overlapRange(amin,amax, bmin,bmax, eps))
		{
			
			return false;
		}
	}
	return true;
}



void dtRandomPointInConvexPoly(const float* pts, const int npts, float* areas,
							   const float s, const float t, float* out)
{
	
	float areasum = 0.0f;
	for (int i = 2; i < npts; i++) {
		areas[i] = dtTriArea2D(&pts[0], &pts[(i-1)*3], &pts[i*3]);
		areasum += dtMax(0.001f, areas[i]);
	}
	
	const float thr = s*areasum;
	float acc = 0.0f;
	float u = 0.0f;
	int tri = 0;
	for (int i = 2; i < npts; i++) {
		const float dacc = areas[i];
		if (thr >= acc && thr < (acc+dacc))
		{
			u = (thr - acc) / dacc;
			tri = i;
			break;
		}
		acc += dacc;
	}
	
	float v = dtSqrt(t);
	
	const float a = 1 - v;
	const float b = (1 - u) * v;
	const float c = u * v;
	const float* pa = &pts[0];
	const float* pb = &pts[(tri-1)*3];
	const float* pc = &pts[tri*3];
	
	out[0] = a*pa[0] + b*pb[0] + c*pc[0];
	out[1] = a*pa[1] + b*pb[1] + c*pc[1];
	out[2] = a*pa[2] + b*pb[2] + c*pc[2];
}

inline float vperpXZ(const float* a, const float* b) { return a[0]*b[2] - a[2]*b[0]; }

bool dtIntersectSegSeg2D(const float* ap, const float* aq,
						 const float* bp, const float* bq,
						 float& s, float& t)
{
	float u[3], v[3], w[3];
	dtVsub(u,aq,ap);
	dtVsub(v,bq,bp);
	dtVsub(w,ap,bp);
	float d = vperpXZ(u,v);
	if (fabsf(d) < 1e-6f) return false;
	s = vperpXZ(v,w) / d;
	t = vperpXZ(u,w) / d;
	return true;
}

