





















































































































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




















#pragma once




























































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































#pragma once



























































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































#pragma pack(push,8)


extern "C" {




















typedef int (__cdecl * _onexit_t)(void);






















typedef struct _div_t {
        int quot;
        int rem;
} div_t;

typedef struct _ldiv_t {
        long quot;
        long rem;
} ldiv_t;

typedef struct _lldiv_t {
        long long quot;
        long long rem;
} lldiv_t;













#pragma pack(4)
typedef struct {
    unsigned char ld[10];
} _LDOUBLE;
#pragma pack()















typedef struct {
        double x;
} _CRT_DOUBLE;

typedef struct {
    float f;
} _CRT_FLOAT;





typedef struct {
        


        long double x;
} _LONGDOUBLE;



#pragma pack(4)
typedef struct {
    unsigned char ld12[12];
} _LDBL12;
#pragma pack()






















 extern int __mb_cur_max;




 int __cdecl ___mb_cur_max_func(void);
 int __cdecl ___mb_cur_max_l_func(_locale_t);






































typedef void (__cdecl *_purecall_handler)(void);


 _purecall_handler __cdecl _set_purecall_handler(  _purecall_handler _Handler);
 _purecall_handler __cdecl _get_purecall_handler(void);



extern "C++"
{





}




typedef void (__cdecl *_invalid_parameter_handler)(const wchar_t *, const wchar_t *, const wchar_t *, unsigned int, uintptr_t);


 _invalid_parameter_handler __cdecl _set_invalid_parameter_handler(  _invalid_parameter_handler _Handler);
 _invalid_parameter_handler __cdecl _get_invalid_parameter_handler(void);





 extern int * __cdecl _errno(void);


errno_t __cdecl _set_errno(  int _Value);
errno_t __cdecl _get_errno(  int * _Value);


 unsigned long * __cdecl __doserrno(void);


errno_t __cdecl _set_doserrno(  unsigned long _Value);
errno_t __cdecl _get_doserrno(  unsigned long * _Value);


 __declspec(deprecated("This function or variable may be unsafe. Consider using " "strerror" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details.")) char ** __cdecl __sys_errlist(void);


 __declspec(deprecated("This function or variable may be unsafe. Consider using " "strerror" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details.")) int * __cdecl __sys_nerr(void);

























 extern int __argc;          
 extern char ** __argv;      
 extern wchar_t ** __wargv;  















 extern char ** _environ;    
 extern wchar_t ** _wenviron;    



__declspec(deprecated("This function or variable may be unsafe. Consider using " "_get_pgmptr" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  extern char * _pgmptr;      
__declspec(deprecated("This function or variable may be unsafe. Consider using " "_get_wpgmptr" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  extern wchar_t * _wpgmptr;  


























errno_t __cdecl _get_pgmptr(  char ** _Value);
errno_t __cdecl _get_wpgmptr(  wchar_t ** _Value);





__declspec(deprecated("This function or variable may be unsafe. Consider using " "_get_fmode" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  extern int _fmode;          






 errno_t __cdecl _set_fmode(  int _Mode);
 errno_t __cdecl _get_fmode(  int * _PMode);






extern "C++"
{
template <typename _CountofType, size_t _SizeOfArray>
char (*__countof_helper( _CountofType (&_Array)[_SizeOfArray]))[_SizeOfArray];

}









 __declspec(noreturn) void __cdecl exit(  int _Code);

 __declspec(noreturn) void __cdecl _exit(  int _Code);
 __declspec(noreturn) void __cdecl abort(void);


 unsigned int __cdecl _set_abort_behavior(  unsigned int _Flags,   unsigned int _Mask);

int       __cdecl abs(  int _X);
long      __cdecl labs(  long _X);
long long __cdecl llabs(  long long _X);

        __int64    __cdecl _abs64(__int64);





































        int    __cdecl atexit(void (__cdecl *)(void));

   double  __cdecl atof(  const char *_String);
   double  __cdecl _atof_l(  const char *_String,   _locale_t _Locale);
    int    __cdecl atoi(  const char *_Str);
   int    __cdecl _atoi_l(  const char *_Str,   _locale_t _Locale);
   long   __cdecl atol(  const char *_Str);
   long   __cdecl _atol_l(  const char *_Str,   _locale_t _Locale);
   long long __cdecl atoll(  const char *_Str);
   long long __cdecl _atoll_l(  const char *_Str,   _locale_t _Locale);



   void * __cdecl bsearch_s(  const void * _Key,   const void * _Base,
          rsize_t _NumOfElements,   rsize_t _SizeOfElements,
          int (__cdecl * _PtFuncCompare)(void *, const void *, const void *), void * _Context);

   void * __cdecl bsearch(  const void * _Key,   const void * _Base,
          size_t _NumOfElements,   size_t _SizeOfElements,
          int (__cdecl * _PtFuncCompare)(const void *, const void *));


 void __cdecl qsort_s(  void * _Base,
          rsize_t _NumOfElements,   rsize_t _SizeOfElements,
          int (__cdecl * _PtFuncCompare)(void *, const void *, const void *), void *_Context);

 void __cdecl qsort(  void * _Base,
          size_t _NumOfElements,   size_t _SizeOfElements,
          int (__cdecl * _PtFuncCompare)(const void *, const void *));

          unsigned short __cdecl _byteswap_ushort(  unsigned short _Short);
          unsigned long  __cdecl _byteswap_ulong (  unsigned long _Long);
          unsigned __int64 __cdecl _byteswap_uint64(  unsigned __int64 _Int64);
   div_t  __cdecl div(  int _Numerator,   int _Denominator);


   __declspec(deprecated("This function or variable may be unsafe. Consider using " "_dupenv_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details.")) char * __cdecl getenv(  const char * _VarName);

  errno_t __cdecl getenv_s(  size_t * _ReturnSize,   char * _DstBuf,   rsize_t _DstSize,   const char * _VarName);

extern "C++" { template <size_t _Size> inline errno_t __cdecl getenv_s(  size_t * _ReturnSize, char (&_Dest)[_Size],   const char * _VarName) throw() { return getenv_s(_ReturnSize, _Dest, _Size, _VarName); } }





  errno_t __cdecl _dupenv_s(    char **_PBuffer,   size_t * _PBufferSizeInBytes,   const char * _VarName);






  errno_t __cdecl _itoa_s(  int _Value,   char * _DstBuf,   size_t _Size,   int _Radix);
extern "C++" { template <size_t _Size> inline errno_t __cdecl _itoa_s(  int _Value, char (&_Dest)[_Size],   int _Radix) throw() { return _itoa_s(_Value, _Dest, _Size, _Radix); } }
__declspec(deprecated("This function or variable may be unsafe. Consider using " "_itoa_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  char * __cdecl _itoa( int _Value,   char *_Dest,  int _Radix);
  errno_t __cdecl _i64toa_s(  __int64 _Val,   char * _DstBuf,   size_t _Size,   int _Radix);
 __declspec(deprecated("This function or variable may be unsafe. Consider using " "_i64toa_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details.")) char * __cdecl _i64toa(  __int64 _Val,     char * _DstBuf,   int _Radix);
  errno_t __cdecl _ui64toa_s(  unsigned __int64 _Val,   char * _DstBuf,   size_t _Size,   int _Radix);
 __declspec(deprecated("This function or variable may be unsafe. Consider using " "_ui64toa_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details.")) char * __cdecl _ui64toa(  unsigned __int64 _Val,     char * _DstBuf,   int _Radix);
   __int64 __cdecl _atoi64(  const char * _String);
   __int64 __cdecl _atoi64_l(  const char * _String,   _locale_t _Locale);
   __int64 __cdecl _strtoi64(  const char * _String,     char ** _EndPtr,   int _Radix);
   __int64 __cdecl _strtoi64_l(  const char * _String,     char ** _EndPtr,   int _Radix,   _locale_t _Locale);
   unsigned __int64 __cdecl _strtoui64(  const char * _String,     char ** _EndPtr,   int _Radix);
   unsigned __int64 __cdecl _strtoui64_l(  const char * _String,     char ** _EndPtr,   int  _Radix,   _locale_t _Locale);
   ldiv_t __cdecl ldiv(  long _Numerator,   long _Denominator);
   lldiv_t __cdecl lldiv(  long long _Numerator,   long long _Denominator);

extern "C++"
{
    inline long abs(long _X) throw()
    {
        return labs(_X);
    }
    inline long long abs(long long _X) throw()
    {
        return llabs(_X);
    }
    inline ldiv_t div(long _A1, long _A2) throw()
    {
        return ldiv(_A1, _A2);
    }
    inline lldiv_t div(long long _A1, long long _A2) throw()
    {
        return lldiv(_A1, _A2);
    }
}

  errno_t __cdecl _ltoa_s(  long _Val,   char * _DstBuf,   size_t _Size,   int _Radix);
extern "C++" { template <size_t _Size> inline errno_t __cdecl _ltoa_s(  long _Value, char (&_Dest)[_Size],   int _Radix) throw() { return _ltoa_s(_Value, _Dest, _Size, _Radix); } }
__declspec(deprecated("This function or variable may be unsafe. Consider using " "_ltoa_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  char * __cdecl _ltoa( long _Value,   char *_Dest,  int _Radix);
   int    __cdecl mblen(    const char * _Ch,   size_t _MaxCount);
   int    __cdecl _mblen_l(    const char * _Ch,   size_t _MaxCount,   _locale_t _Locale);
   size_t __cdecl _mbstrlen(  const char * _Str);
   size_t __cdecl _mbstrlen_l(  const char *_Str,   _locale_t _Locale);
   size_t __cdecl _mbstrnlen(  const char *_Str,   size_t _MaxCount);
   size_t __cdecl _mbstrnlen_l(  const char *_Str,   size_t _MaxCount,   _locale_t _Locale);
 int    __cdecl mbtowc(    wchar_t * _DstCh,     const char * _SrcCh,   size_t _SrcSizeInBytes);
 int    __cdecl _mbtowc_l(    wchar_t * _DstCh,     const char * _SrcCh,   size_t _SrcSizeInBytes,   _locale_t _Locale);
  errno_t __cdecl mbstowcs_s(  size_t * _PtNumOfCharConverted,   wchar_t * _DstBuf,   size_t _SizeInWords,   const char * _SrcBuf,   size_t _MaxCount );
extern "C++" { template <size_t _Size> inline errno_t __cdecl mbstowcs_s(  size_t * _PtNumOfCharConverted,   wchar_t (&_Dest)[_Size],   const char * _Source,   size_t _MaxCount) throw() { return mbstowcs_s(_PtNumOfCharConverted, _Dest, _Size, _Source, _MaxCount); } }
__declspec(deprecated("This function or variable may be unsafe. Consider using " "mbstowcs_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  size_t __cdecl mbstowcs( wchar_t *_Dest,  const char * _Source,  size_t _MaxCount);

  errno_t __cdecl _mbstowcs_s_l(  size_t * _PtNumOfCharConverted,   wchar_t * _DstBuf,   size_t _SizeInWords,   const char * _SrcBuf,   size_t _MaxCount,   _locale_t _Locale);
extern "C++" { template <size_t _Size> inline errno_t __cdecl _mbstowcs_s_l(  size_t * _PtNumOfCharConverted, wchar_t (&_Dest)[_Size],   const char * _Source,   size_t _MaxCount,   _locale_t _Locale) throw() { return _mbstowcs_s_l(_PtNumOfCharConverted, _Dest, _Size, _Source, _MaxCount, _Locale); } }
__declspec(deprecated("This function or variable may be unsafe. Consider using " "_mbstowcs_s_l" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  size_t __cdecl _mbstowcs_l(  wchar_t *_Dest,   const char * _Source,   size_t _MaxCount,   _locale_t _Locale);

   int    __cdecl rand(void);




  int    __cdecl _set_error_mode(  int _Mode);

 void   __cdecl srand(  unsigned int _Seed);
   double __cdecl strtod(  const char * _Str,     char ** _EndPtr);
   double __cdecl _strtod_l(  const char * _Str,     char ** _EndPtr,   _locale_t _Locale);
   long   __cdecl strtol(  const char * _Str,     char ** _EndPtr,   int _Radix );
   long   __cdecl _strtol_l(  const char *_Str,     char **_EndPtr,   int _Radix,   _locale_t _Locale);
   long long  __cdecl strtoll(  const char * _Str,     char ** _EndPtr,   int _Radix );
   long long  __cdecl _strtoll_l(  const char * _Str,     char ** _EndPtr,   int _Radix,   _locale_t _Locale );
   unsigned long __cdecl strtoul(  const char * _Str,     char ** _EndPtr,   int _Radix);
   unsigned long __cdecl _strtoul_l(const char * _Str,     char **_EndPtr,   int _Radix,   _locale_t _Locale);
   unsigned long long __cdecl strtoull(  const char * _Str,     char ** _EndPtr,   int _Radix);
   unsigned long long __cdecl _strtoull_l(  const char * _Str,     char ** _EndPtr,   int _Radix,   _locale_t _Locale);
   long double __cdecl strtold(  const char * _Str,     char ** _EndPtr);
   long double __cdecl _strtold_l(  const char * _Str,     char ** _EndPtr,   _locale_t _Locale);
   float __cdecl strtof(  const char * _Str,     char ** _EndPtr);
   float __cdecl _strtof_l(  const char * _Str,     char ** _EndPtr,   _locale_t _Locale);




 int __cdecl system(  const char * _Command);



  errno_t __cdecl _ultoa_s(  unsigned long _Val,   char * _DstBuf,   size_t _Size,   int _Radix);
extern "C++" { template <size_t _Size> inline errno_t __cdecl _ultoa_s(  unsigned long _Value, char (&_Dest)[_Size],   int _Radix) throw() { return _ultoa_s(_Value, _Dest, _Size, _Radix); } }
__declspec(deprecated("This function or variable may be unsafe. Consider using " "_ultoa_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  char * __cdecl _ultoa( unsigned long _Value,   char *_Dest,  int _Radix);
 __declspec(deprecated("This function or variable may be unsafe. Consider using " "wctomb_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details.")) int    __cdecl wctomb(  char * _MbCh,   wchar_t _WCh);
 __declspec(deprecated("This function or variable may be unsafe. Consider using " "_wctomb_s_l" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details.")) int    __cdecl _wctomb_l(    char * _MbCh,   wchar_t _WCh,   _locale_t _Locale);

  errno_t __cdecl wctomb_s(  int * _SizeConverted,   char * _MbCh,   rsize_t _SizeInBytes,   wchar_t _WCh);

  errno_t __cdecl _wctomb_s_l(  int * _SizeConverted,   char * _MbCh,   size_t _SizeInBytes,   wchar_t _WCh,   _locale_t _Locale);
  errno_t __cdecl wcstombs_s(  size_t * _PtNumOfCharConverted,   char * _Dst,   size_t _DstSizeInBytes,   const wchar_t * _Src,   size_t _MaxCountInBytes);
extern "C++" { template <size_t _Size> inline errno_t __cdecl wcstombs_s(  size_t * _PtNumOfCharConverted,   char (&_Dest)[_Size],   const wchar_t * _Source,   size_t _MaxCount) throw() { return wcstombs_s(_PtNumOfCharConverted, _Dest, _Size, _Source, _MaxCount); } }
__declspec(deprecated("This function or variable may be unsafe. Consider using " "wcstombs_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  size_t __cdecl wcstombs( char *_Dest,  const wchar_t * _Source,  size_t _MaxCount);
  errno_t __cdecl _wcstombs_s_l(  size_t * _PtNumOfCharConverted,   char * _Dst,   size_t _DstSizeInBytes,   const wchar_t * _Src,   size_t _MaxCountInBytes,   _locale_t _Locale);
extern "C++" { template <size_t _Size> inline errno_t __cdecl _wcstombs_s_l(  size_t * _PtNumOfCharConverted,   char (&_Dest)[_Size],   const wchar_t * _Source,   size_t _MaxCount,   _locale_t _Locale) throw() { return _wcstombs_s_l(_PtNumOfCharConverted, _Dest, _Size, _Source, _MaxCount, _Locale); } }
__declspec(deprecated("This function or variable may be unsafe. Consider using " "_wcstombs_s_l" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  size_t __cdecl _wcstombs_l(  char *_Dest,   const wchar_t * _Source,   size_t _MaxCount,   _locale_t _Locale);





























































        __declspec(noalias) __declspec(restrict)    void * __cdecl calloc(   size_t _Count,    size_t _Size);
                     __declspec(noalias)                                                                             void   __cdecl free(    void * _Memory);
        __declspec(noalias) __declspec(restrict)                              void * __cdecl malloc(   size_t _Size);
 
       __declspec(noalias) __declspec(restrict)                           void * __cdecl realloc(    void * _Memory,    size_t _NewSize);
 
       __declspec(noalias) __declspec(restrict)                       void * __cdecl _recalloc(    void * _Memory,    size_t _Count,    size_t _Size);
                     __declspec(noalias)                                                                             void   __cdecl _aligned_free(    void * _Memory);
       __declspec(noalias) __declspec(restrict)                              void * __cdecl _aligned_malloc(   size_t _Size,   size_t _Alignment);
       __declspec(noalias) __declspec(restrict)                              void * __cdecl _aligned_offset_malloc(   size_t _Size,   size_t _Alignment,   size_t _Offset);
 
       __declspec(noalias) __declspec(restrict)                              void * __cdecl _aligned_realloc(    void * _Memory,    size_t _NewSize,   size_t _Alignment);
 
       __declspec(noalias) __declspec(restrict)                       void * __cdecl _aligned_recalloc(    void * _Memory,    size_t _Count,    size_t _Size,   size_t _Alignment);
 
       __declspec(noalias) __declspec(restrict)                              void * __cdecl _aligned_offset_realloc(    void * _Memory,    size_t _NewSize,   size_t _Alignment,   size_t _Offset);
 
       __declspec(noalias) __declspec(restrict)                       void * __cdecl _aligned_offset_recalloc(    void * _Memory,    size_t _Count,    size_t _Size,   size_t _Alignment,   size_t _Offset);
                                                    size_t __cdecl _aligned_msize(  void * _Memory,   size_t _Alignment,   size_t _Offset);


























  errno_t __cdecl _itow_s (  int _Val,   wchar_t * _DstBuf,   size_t _SizeInWords,   int _Radix);
extern "C++" { template <size_t _Size> inline errno_t __cdecl _itow_s(  int _Value, wchar_t (&_Dest)[_Size],   int _Radix) throw() { return _itow_s(_Value, _Dest, _Size, _Radix); } }
__declspec(deprecated("This function or variable may be unsafe. Consider using " "_itow_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  wchar_t * __cdecl _itow( int _Value,   wchar_t *_Dest,  int _Radix);
  errno_t __cdecl _ltow_s (  long _Val,   wchar_t * _DstBuf,   size_t _SizeInWords,   int _Radix);
extern "C++" { template <size_t _Size> inline errno_t __cdecl _ltow_s(  long _Value, wchar_t (&_Dest)[_Size],   int _Radix) throw() { return _ltow_s(_Value, _Dest, _Size, _Radix); } }
__declspec(deprecated("This function or variable may be unsafe. Consider using " "_ltow_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  wchar_t * __cdecl _ltow( long _Value,   wchar_t *_Dest,  int _Radix);
  errno_t __cdecl _ultow_s (  unsigned long _Val,   wchar_t * _DstBuf,   size_t _SizeInWords,   int _Radix);
extern "C++" { template <size_t _Size> inline errno_t __cdecl _ultow_s(  unsigned long _Value, wchar_t (&_Dest)[_Size],   int _Radix) throw() { return _ultow_s(_Value, _Dest, _Size, _Radix); } }
__declspec(deprecated("This function or variable may be unsafe. Consider using " "_ultow_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  wchar_t * __cdecl _ultow( unsigned long _Value,   wchar_t *_Dest,  int _Radix);
   double __cdecl wcstod(  const wchar_t * _Str,     wchar_t ** _EndPtr);
   double __cdecl _wcstod_l(  const wchar_t *_Str,     wchar_t ** _EndPtr,   _locale_t _Locale);
   long   __cdecl wcstol(  const wchar_t *_Str,     wchar_t ** _EndPtr, int _Radix);
   long   __cdecl _wcstol_l(  const wchar_t *_Str,     wchar_t **_EndPtr, int _Radix,   _locale_t _Locale);
   long long  __cdecl wcstoll(  const wchar_t *_Str,     wchar_t **_EndPtr, int _Radix);
   long long  __cdecl _wcstoll_l(  const wchar_t *_Str,     wchar_t **_EndPtr, int _Radix,   _locale_t _Locale);
   unsigned long __cdecl wcstoul(  const wchar_t *_Str,     wchar_t ** _EndPtr, int _Radix);
   unsigned long __cdecl _wcstoul_l(  const wchar_t *_Str,     wchar_t **_EndPtr, int _Radix,   _locale_t _Locale);
   unsigned long long __cdecl wcstoull(  const wchar_t *_Str,     wchar_t ** _EndPtr, int _Radix);
   unsigned long long __cdecl _wcstoull_l(  const wchar_t *_Str,     wchar_t ** _EndPtr, int _Radix,   _locale_t _Locale);
   long double __cdecl wcstold(  const wchar_t * _Str,     wchar_t ** _EndPtr);
   long double __cdecl _wcstold_l(  const wchar_t * _Str,     wchar_t ** _EndPtr,   _locale_t _Locale);
   float __cdecl wcstof(  const wchar_t * _Str,     wchar_t ** _EndPtr);
   float __cdecl _wcstof_l(  const wchar_t * _Str,     wchar_t ** _EndPtr,   _locale_t _Locale);



   __declspec(deprecated("This function or variable may be unsafe. Consider using " "_wdupenv_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details.")) wchar_t * __cdecl _wgetenv(  const wchar_t * _VarName);
  errno_t __cdecl _wgetenv_s(  size_t * _ReturnSize,   wchar_t * _DstBuf,   size_t _DstSizeInWords,   const wchar_t * _VarName);
extern "C++" { template <size_t _Size> inline errno_t __cdecl _wgetenv_s(  size_t * _ReturnSize, wchar_t (&_Dest)[_Size],   const wchar_t * _VarName) throw() { return _wgetenv_s(_ReturnSize, _Dest, _Size, _VarName); } }






  errno_t __cdecl _wdupenv_s(    wchar_t **_Buffer,   size_t *_BufferSizeInWords,   const wchar_t *_VarName);







 int __cdecl _wsystem(  const wchar_t * _Command);




   double __cdecl _wtof(  const wchar_t *_Str);
   double __cdecl _wtof_l(  const wchar_t *_Str,   _locale_t _Locale);
   int __cdecl _wtoi(  const wchar_t *_Str);
   int __cdecl _wtoi_l(  const wchar_t *_Str,   _locale_t _Locale);
   long __cdecl _wtol(  const wchar_t *_Str);
   long __cdecl _wtol_l(  const wchar_t *_Str,   _locale_t _Locale);
   long long __cdecl _wtoll(  const wchar_t *_Str);
   long long __cdecl _wtoll_l(  const wchar_t *_Str,   _locale_t _Locale);

  errno_t __cdecl _i64tow_s(  __int64 _Val,   wchar_t * _DstBuf,   size_t _SizeInWords,   int _Radix);
 __declspec(deprecated("This function or variable may be unsafe. Consider using " "_i64tow_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details.")) wchar_t * __cdecl _i64tow(  __int64 _Val,     wchar_t * _DstBuf,   int _Radix);
  errno_t __cdecl _ui64tow_s(  unsigned __int64 _Val,   wchar_t * _DstBuf,   size_t _SizeInWords,   int _Radix);
 __declspec(deprecated("This function or variable may be unsafe. Consider using " "_ui64tow_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details.")) wchar_t * __cdecl _ui64tow(  unsigned __int64 _Val,     wchar_t * _DstBuf,   int _Radix);
   __int64   __cdecl _wtoi64(  const wchar_t *_Str);
   __int64   __cdecl _wtoi64_l(  const wchar_t *_Str,   _locale_t _Locale);
   __int64   __cdecl _wcstoi64(  const wchar_t * _Str,     wchar_t ** _EndPtr,   int _Radix);
   __int64   __cdecl _wcstoi64_l(  const wchar_t * _Str,     wchar_t ** _EndPtr,   int _Radix,   _locale_t _Locale);
   unsigned __int64  __cdecl _wcstoui64(  const wchar_t * _Str,     wchar_t ** _EndPtr,   int _Radix);
   unsigned __int64  __cdecl _wcstoui64_l(  const wchar_t *_Str ,     wchar_t ** _EndPtr,   int _Radix,   _locale_t _Locale);


















   char * __cdecl _fullpath(  char * _FullPath,   const char * _Path,   size_t _SizeInBytes);







  errno_t __cdecl _ecvt_s(  char * _DstBuf,   size_t _Size,   double _Val,   int _NumOfDights,   int * _PtDec,   int * _PtSign);
extern "C++" { template <size_t _Size> inline errno_t __cdecl _ecvt_s(char (&_Dest)[_Size],   double _Value,   int _NumOfDigits,   int * _PtDec,   int * _PtSign) throw() { return _ecvt_s(_Dest, _Size, _Value, _NumOfDigits, _PtDec, _PtSign); } }
   __declspec(deprecated("This function or variable may be unsafe. Consider using " "_ecvt_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details.")) char * __cdecl _ecvt(  double _Val,   int _NumOfDigits,   int * _PtDec,   int * _PtSign);
  errno_t __cdecl _fcvt_s(  char * _DstBuf,   size_t _Size,   double _Val,   int _NumOfDec,   int * _PtDec,   int * _PtSign);
extern "C++" { template <size_t _Size> inline errno_t __cdecl _fcvt_s(char (&_Dest)[_Size],   double _Value,   int _NumOfDigits,   int * _PtDec,   int * _PtSign) throw() { return _fcvt_s(_Dest, _Size, _Value, _NumOfDigits, _PtDec, _PtSign); } }
   __declspec(deprecated("This function or variable may be unsafe. Consider using " "_fcvt_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details.")) char * __cdecl _fcvt(  double _Val,   int _NumOfDec,   int * _PtDec,   int * _PtSign);
 errno_t __cdecl _gcvt_s(  char * _DstBuf,   size_t _Size,   double _Val,   int _NumOfDigits);
extern "C++" { template <size_t _Size> inline errno_t __cdecl _gcvt_s(char (&_Dest)[_Size],   double _Value,   int _NumOfDigits) throw() { return _gcvt_s(_Dest, _Size, _Value, _NumOfDigits); } }
 __declspec(deprecated("This function or variable may be unsafe. Consider using " "_gcvt_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details.")) char * __cdecl _gcvt(  double _Val,   int _NumOfDigits,     char * _DstBuf);

   int __cdecl _atodbl(  _CRT_DOUBLE * _Result,   char * _Str);
   int __cdecl _atoldbl(  _LDOUBLE * _Result,   char * _Str);
   int __cdecl _atoflt(  _CRT_FLOAT * _Result,   const char * _Str);
   int __cdecl _atodbl_l(  _CRT_DOUBLE * _Result,   char * _Str,   _locale_t _Locale);
   int __cdecl _atoldbl_l(  _LDOUBLE * _Result,   char * _Str,   _locale_t _Locale);
   int __cdecl _atoflt_l(  _CRT_FLOAT * _Result,   const char * _Str,   _locale_t _Locale);
          unsigned long __cdecl _lrotl(  unsigned long _Val,   int _Shift);
          unsigned long __cdecl _lrotr(  unsigned long _Val,   int _Shift);
  errno_t   __cdecl _makepath_s(  char * _PathResult,   size_t _SizeInWords,   const char * _Drive,   const char * _Dir,   const char * _Filename,
          const char * _Ext);
extern "C++" { template <size_t _Size> inline errno_t __cdecl _makepath_s(char (&_Path)[_Size],   const char * _Drive,   const char * _Dir,   const char * _Filename,   const char * _Ext) throw() { return _makepath_s(_Path, _Size, _Drive, _Dir, _Filename, _Ext); } }
__declspec(deprecated("This function or variable may be unsafe. Consider using " "_makepath_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  void __cdecl _makepath(  char *_Path,  const char * _Drive,  const char * _Dir,  const char * _Filename,  const char * _Ext);


























        _onexit_t __cdecl _onexit(  _onexit_t _Func);







#pragma warning (push)
#pragma warning (disable:6540) 


   int    __cdecl _putenv(  const char * _EnvString);
  errno_t __cdecl _putenv_s(  const char * _Name,   const char * _Value);


        unsigned int __cdecl _rotl(  unsigned int _Val,   int _Shift);
        unsigned __int64 __cdecl _rotl64(  unsigned __int64 _Val,   int _Shift);
        unsigned int __cdecl _rotr(  unsigned int _Val,   int _Shift);
        unsigned __int64 __cdecl _rotr64(  unsigned __int64 _Val,   int _Shift);
#pragma warning (pop)


 errno_t __cdecl _searchenv_s(  const char * _Filename,   const char * _EnvVar,   char * _ResultPath,   size_t _SizeInBytes);
extern "C++" { template <size_t _Size> inline errno_t __cdecl _searchenv_s(  const char * _Filename,   const char * _EnvVar, char (&_ResultPath)[_Size]) throw() { return _searchenv_s(_Filename, _EnvVar, _ResultPath, _Size); } }
__declspec(deprecated("This function or variable may be unsafe. Consider using " "_searchenv_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  void __cdecl _searchenv( const char * _Filename,  const char * _EnvVar,   char *_ResultPath);


__declspec(deprecated("This function or variable may be unsafe. Consider using " "_splitpath_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  void   __cdecl _splitpath(  const char * _FullPath,     char * _Drive,     char * _Dir,     char * _Filename,     char * _Ext);
  errno_t  __cdecl _splitpath_s(  const char * _FullPath,
                  char * _Drive,   size_t _DriveSize,
                  char * _Dir,   size_t _DirSize,
                  char * _Filename,   size_t _FilenameSize,
                  char * _Ext,   size_t _ExtSize);
extern "C++" { template <size_t _DriveSize, size_t _DirSize, size_t _NameSize, size_t _ExtSize> inline errno_t __cdecl _splitpath_s(  const char *_Dest,   char (&_Drive)[_DriveSize],   char (&_Dir)[_DirSize],   char (&_Name)[_NameSize],   char (&_Ext)[_ExtSize]) throw() { return _splitpath_s(_Dest, _Drive, _DriveSize, _Dir, _DirSize, _Name, _NameSize, _Ext, _ExtSize); } }

 void   __cdecl _swab(    char * _Buf1,     char * _Buf2, int _SizeInBytes);










   wchar_t * __cdecl _wfullpath(  wchar_t * _FullPath,   const wchar_t * _Path,   size_t _SizeInWords);





  errno_t __cdecl _wmakepath_s(  wchar_t * _PathResult,   size_t _SIZE,   const wchar_t * _Drive,   const wchar_t * _Dir,   const wchar_t * _Filename,
          const wchar_t * _Ext);
extern "C++" { template <size_t _Size> inline errno_t __cdecl _wmakepath_s(wchar_t (&_ResultPath)[_Size],   const wchar_t * _Drive,   const wchar_t * _Dir,   const wchar_t * _Filename,   const wchar_t * _Ext) throw() { return _wmakepath_s(_ResultPath, _Size, _Drive, _Dir, _Filename, _Ext); } }
__declspec(deprecated("This function or variable may be unsafe. Consider using " "_wmakepath_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  void __cdecl _wmakepath(  wchar_t *_ResultPath,  const wchar_t * _Drive,  const wchar_t * _Dir,  const wchar_t * _Filename,  const wchar_t * _Ext);






   int    __cdecl _wputenv(  const wchar_t * _EnvString);
  errno_t __cdecl _wputenv_s(  const wchar_t * _Name,   const wchar_t * _Value);
 errno_t __cdecl _wsearchenv_s(  const wchar_t * _Filename,   const wchar_t * _EnvVar,   wchar_t * _ResultPath,   size_t _SizeInWords);
extern "C++" { template <size_t _Size> inline errno_t __cdecl _wsearchenv_s(  const wchar_t * _Filename,   const wchar_t * _EnvVar, wchar_t (&_ResultPath)[_Size]) throw() { return _wsearchenv_s(_Filename, _EnvVar, _ResultPath, _Size); } }
__declspec(deprecated("This function or variable may be unsafe. Consider using " "_wsearchenv_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  void __cdecl _wsearchenv( const wchar_t * _Filename,  const wchar_t * _EnvVar,   wchar_t *_ResultPath);


__declspec(deprecated("This function or variable may be unsafe. Consider using " "_wsplitpath_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  void   __cdecl _wsplitpath(  const wchar_t * _FullPath,     wchar_t * _Drive,     wchar_t * _Dir,     wchar_t * _Filename,     wchar_t * _Ext);
 errno_t __cdecl _wsplitpath_s(  const wchar_t * _FullPath,
                  wchar_t * _Drive,   size_t _DriveSize,
                  wchar_t * _Dir,   size_t _DirSize,
                  wchar_t * _Filename,   size_t _FilenameSize,
                  wchar_t * _Ext,   size_t _ExtSize);
extern "C++" { template <size_t _DriveSize, size_t _DirSize, size_t _NameSize, size_t _ExtSize> inline errno_t __cdecl _wsplitpath_s(  const wchar_t *_Path,   wchar_t (&_Drive)[_DriveSize],   wchar_t (&_Dir)[_DirSize],   wchar_t (&_Name)[_NameSize],   wchar_t (&_Ext)[_ExtSize]) throw() { return _wsplitpath_s(_Path, _Drive, _DriveSize, _Dir, _DirSize, _Name, _NameSize, _Ext, _ExtSize); } }






__declspec(deprecated("This function or variable has been superceded by newer library or operating system functionality. Consider using " "SetErrorMode" " instead. See online help for details."))  void __cdecl _seterrormode(  int _Mode);
__declspec(deprecated("This function or variable has been superceded by newer library or operating system functionality. Consider using " "Beep" " instead. See online help for details."))  void __cdecl _beep(  unsigned _Frequency,   unsigned _Duration);
__declspec(deprecated("This function or variable has been superceded by newer library or operating system functionality. Consider using " "Sleep" " instead. See online help for details."))  void __cdecl _sleep(  unsigned long _Duration);




















#pragma warning(push)
#pragma warning(disable: 4141) 
  __declspec(deprecated("The POSIX name for this item is deprecated. Instead, use the ISO C++ conformant name: " "_ecvt" ". See online help for details.")) __declspec(deprecated("This function or variable may be unsafe. Consider using " "_ecvt_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  char * __cdecl ecvt(  double _Val,   int _NumOfDigits,   int * _PtDec,   int * _PtSign);
  __declspec(deprecated("The POSIX name for this item is deprecated. Instead, use the ISO C++ conformant name: " "_fcvt" ". See online help for details.")) __declspec(deprecated("This function or variable may be unsafe. Consider using " "_fcvt_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  char * __cdecl fcvt(  double _Val,   int _NumOfDec,   int * _PtDec,   int * _PtSign);
__declspec(deprecated("The POSIX name for this item is deprecated. Instead, use the ISO C++ conformant name: " "_gcvt" ". See online help for details.")) __declspec(deprecated("This function or variable may be unsafe. Consider using " "_fcvt_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))           char * __cdecl gcvt(  double _Val,   int _NumOfDigits,     char * _DstBuf);
__declspec(deprecated("The POSIX name for this item is deprecated. Instead, use the ISO C++ conformant name: " "_itoa" ". See online help for details.")) __declspec(deprecated("This function or variable may be unsafe. Consider using " "_itoa_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))           char * __cdecl itoa(  int _Val,     char * _DstBuf,   int _Radix);
__declspec(deprecated("The POSIX name for this item is deprecated. Instead, use the ISO C++ conformant name: " "_ltoa" ". See online help for details.")) __declspec(deprecated("This function or variable may be unsafe. Consider using " "_ltoa_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))           char * __cdecl ltoa(  long _Val,     char * _DstBuf,   int _Radix);


  __declspec(deprecated("The POSIX name for this item is deprecated. Instead, use the ISO C++ conformant name: " "_putenv" ". See online help for details."))  int    __cdecl putenv(  const char * _EnvString);


__declspec(deprecated("The POSIX name for this item is deprecated. Instead, use the ISO C++ conformant name: " "_swab" ". See online help for details."))                                                                            void   __cdecl swab(  char * _Buf1,  char * _Buf2,   int _SizeInBytes);
__declspec(deprecated("The POSIX name for this item is deprecated. Instead, use the ISO C++ conformant name: " "_ultoa" ". See online help for details.")) __declspec(deprecated("This function or variable may be unsafe. Consider using " "_ultoa_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))         char * __cdecl ultoa(  unsigned long _Val,     char * _Dstbuf,   int _Radix);
#pragma warning(pop)
_onexit_t __cdecl onexit(  _onexit_t _Func);





}



#pragma pack(pop)



















#pragma once















































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































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





















#pragma once

























































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































#pragma once












































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































extern "C" {














































































  unsigned int __cdecl _clearfp(void);
#pragma warning(push)
#pragma warning(disable: 4141)
 __declspec(deprecated("This function or variable may be unsafe. Consider using " "_controlfp_s" " instead. To disable deprecation, use _CRT_SECURE_NO_WARNINGS. See online help for details."))  unsigned int __cdecl _controlfp(  unsigned int _NewValue,  unsigned int _Mask);
#pragma warning(pop)
  void __cdecl _set_controlfp(  unsigned int _NewValue,   unsigned int _Mask);
  errno_t __cdecl _controlfp_s(  unsigned int *_CurrentState,   unsigned int _NewValue,   unsigned int _Mask);
  unsigned int __cdecl _statusfp(void);
  void __cdecl _fpreset(void);


  void __cdecl _statusfp2(  unsigned int *_X86_status,   unsigned int *_SSE2_status);









































































  unsigned int __cdecl _control87(  unsigned int _NewValue,  unsigned int _Mask);

  int __cdecl __control87_2(  unsigned int _NewValue,   unsigned int _Mask,
                                    unsigned int* _X86_cw,   unsigned int* _Sse2_cw);




   extern int * __cdecl __fpecode(void);






























   double __cdecl _copysign(  double _Number,   double _Sign);
   double __cdecl _chgsign(  double _X);
   double __cdecl _scalb(  double _X,   long _Y);
   double __cdecl _logb(  double _X);
   double __cdecl _nextafter(  double _X,   double _Y);
   int    __cdecl _finite(  double _X);
   int    __cdecl _isnan(  double _X);
   int    __cdecl _fpclass(  double _X);

























  void __cdecl fpreset(void);
































































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












































































struct dtNavMeshCreateParams
{

	
	
	
	

	const unsigned short* verts;			
	int vertCount;							
	const unsigned short* polys;			
	const unsigned short* polyFlags;		
	const unsigned char* polyAreas;			
	int polyCount;							
	int nvp;								

	
	
	
	

	const unsigned int* detailMeshes;		
	const float* detailVerts;				
	int detailVertsCount;					
	const unsigned char* detailTris;		
	int detailTriCount;						

	
	
	
	
	
	

	
	const float* offMeshConVerts;
	
	const float* offMeshConRad;
	
	const unsigned short* offMeshConFlags;
	
	const unsigned char* offMeshConAreas;
	
	
	
	
	const unsigned char* offMeshConDir;	
	
	const unsigned int* offMeshConUserID;
	
	int offMeshConCount;

	
	
	
	

	unsigned int userId;	
	int tileX;				
	int tileY;				
	int tileLayer;			
	float bmin[3];			
	float bmax[3];			

	
	
	

	float walkableHeight;	
	float walkableRadius;	
	float walkableClimb;	
	float cs;				
	float ch;				

	
	
	bool buildBvTree;

	
};







bool dtCreateNavMeshData(dtNavMeshCreateParams* params, unsigned char** outData, int* outDataSize);




bool dtNavMeshHeaderSwapEndian(unsigned char* data, const int dataSize);




bool dtNavMeshDataSwapEndian(unsigned char* data, const int dataSize);

































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































extern "C" {


 void __cdecl _wassert(  const wchar_t * _Message,   const wchar_t *_File,   unsigned _Line);


}












static unsigned short MESH_NULL_IDX = 0xffff;


struct BVItem
{
	unsigned short bmin[3];
	unsigned short bmax[3];
	int i;
};

static int compareItemX(const void* va, const void* vb)
{
	const BVItem* a = (const BVItem*)va;
	const BVItem* b = (const BVItem*)vb;
	if (a->bmin[0] < b->bmin[0])
		return -1;
	if (a->bmin[0] > b->bmin[0])
		return 1;
	return 0;
}

static int compareItemY(const void* va, const void* vb)
{
	const BVItem* a = (const BVItem*)va;
	const BVItem* b = (const BVItem*)vb;
	if (a->bmin[1] < b->bmin[1])
		return -1;
	if (a->bmin[1] > b->bmin[1])
		return 1;
	return 0;
}

static int compareItemZ(const void* va, const void* vb)
{
	const BVItem* a = (const BVItem*)va;
	const BVItem* b = (const BVItem*)vb;
	if (a->bmin[2] < b->bmin[2])
		return -1;
	if (a->bmin[2] > b->bmin[2])
		return 1;
	return 0;
}

static void calcExtends(BVItem* items, const int , const int imin, const int imax,
						unsigned short* bmin, unsigned short* bmax)
{
	bmin[0] = items[imin].bmin[0];
	bmin[1] = items[imin].bmin[1];
	bmin[2] = items[imin].bmin[2];
	
	bmax[0] = items[imin].bmax[0];
	bmax[1] = items[imin].bmax[1];
	bmax[2] = items[imin].bmax[2];
	
	for (int i = imin+1; i < imax; ++i)
	{
		const BVItem& it = items[i];
		if (it.bmin[0] < bmin[0]) bmin[0] = it.bmin[0];
		if (it.bmin[1] < bmin[1]) bmin[1] = it.bmin[1];
		if (it.bmin[2] < bmin[2]) bmin[2] = it.bmin[2];
		
		if (it.bmax[0] > bmax[0]) bmax[0] = it.bmax[0];
		if (it.bmax[1] > bmax[1]) bmax[1] = it.bmax[1];
		if (it.bmax[2] > bmax[2]) bmax[2] = it.bmax[2];
	}
}

inline int longestAxis(unsigned short x, unsigned short y, unsigned short z)
{
	int	axis = 0;
	unsigned short maxVal = x;
	if (y > maxVal)
	{
		axis = 1;
		maxVal = y;
	}
	if (z > maxVal)
	{
		axis = 2;
		maxVal = z;
	}
	return axis;
}

static void subdivide(BVItem* items, int nitems, int imin, int imax, int& curNode, dtBVNode* nodes)
{
	int inum = imax - imin;
	int icur = curNode;
	
	dtBVNode& node = nodes[curNode++];
	
	if (inum == 1)
	{
		
		node.bmin[0] = items[imin].bmin[0];
		node.bmin[1] = items[imin].bmin[1];
		node.bmin[2] = items[imin].bmin[2];
		
		node.bmax[0] = items[imin].bmax[0];
		node.bmax[1] = items[imin].bmax[1];
		node.bmax[2] = items[imin].bmax[2];
		
		node.i = items[imin].i;
	}
	else
	{
		
		calcExtends(items, nitems, imin, imax, node.bmin, node.bmax);
		
		int	axis = longestAxis(node.bmax[0] - node.bmin[0],
							   node.bmax[1] - node.bmin[1],
							   node.bmax[2] - node.bmin[2]);
		
		if (axis == 0)
		{
			
			qsort(items+imin, inum, sizeof(BVItem), compareItemX);
		}
		else if (axis == 1)
		{
			
			qsort(items+imin, inum, sizeof(BVItem), compareItemY);
		}
		else
		{
			
			qsort(items+imin, inum, sizeof(BVItem), compareItemZ);
		}
		
		int isplit = imin+inum/2;
		
		
		subdivide(items, nitems, imin, isplit, curNode, nodes);
		
		subdivide(items, nitems, isplit, imax, curNode, nodes);
		
		int iescape = curNode - icur;
		
		node.i = -iescape;
	}
}

static int createBVTree(const unsigned short* verts, const int ,
						const unsigned short* polys, const int npolys, const int nvp,
						const float cs, const float ch,
						const int , dtBVNode* nodes)
{
	
	BVItem* items = (BVItem*)dtAlloc(sizeof(BVItem)*npolys, DT_ALLOC_TEMP);
	for (int i = 0; i < npolys; i++)
	{
		BVItem& it = items[i];
		it.i = i;
		
		const unsigned short* p = &polys[i*nvp*2];
		it.bmin[0] = it.bmax[0] = verts[p[0]*3+0];
		it.bmin[1] = it.bmax[1] = verts[p[0]*3+1];
		it.bmin[2] = it.bmax[2] = verts[p[0]*3+2];
		
		for (int j = 1; j < nvp; ++j)
		{
			if (p[j] == MESH_NULL_IDX) break;
			unsigned short x = verts[p[j]*3+0];
			unsigned short y = verts[p[j]*3+1];
			unsigned short z = verts[p[j]*3+2];
			
			if (x < it.bmin[0]) it.bmin[0] = x;
			if (y < it.bmin[1]) it.bmin[1] = y;
			if (z < it.bmin[2]) it.bmin[2] = z;
			
			if (x > it.bmax[0]) it.bmax[0] = x;
			if (y > it.bmax[1]) it.bmax[1] = y;
			if (z > it.bmax[2]) it.bmax[2] = z;
		}
		
		it.bmin[1] = (unsigned short)floorf((float)it.bmin[1]*ch/cs);
		it.bmax[1] = (unsigned short)ceilf((float)it.bmax[1]*ch/cs);
	}
	
	int curNode = 0;
	subdivide(items, npolys, 0, npolys, curNode, nodes);
	
	dtFree(items);
	
	return curNode;
}

static unsigned char classifyOffMeshPoint(const float* pt, const float* bmin, const float* bmax)
{
	static const unsigned char XP = 1<<0;
	static const unsigned char ZP = 1<<1;
	static const unsigned char XM = 1<<2;
	static const unsigned char ZM = 1<<3;	

	unsigned char outcode = 0; 
	outcode |= (pt[0] >= bmax[0]) ? XP : 0;
	outcode |= (pt[2] >= bmax[2]) ? ZP : 0;
	outcode |= (pt[0] < bmin[0])  ? XM : 0;
	outcode |= (pt[2] < bmin[2])  ? ZM : 0;

	switch (outcode)
	{
	case XP: return 0;
	case XP|ZP: return 1;
	case ZP: return 2;
	case XM|ZP: return 3;
	case XM: return 4;
	case XM|ZM: return 5;
	case ZM: return 6;
	case XP|ZM: return 7;
	};

	return 0xff;	
}










bool dtCreateNavMeshData(dtNavMeshCreateParams* params, unsigned char** outData, int* outDataSize)
{
	if (params->nvp > DT_VERTS_PER_POLYGON)
		return false;
	if (params->vertCount >= 0xffff)
		return false;
	if (!params->vertCount || !params->verts)
		return false;
	if (!params->polyCount || !params->polys)
		return false;

	const int nvp = params->nvp;
	
	
	
	unsigned char* offMeshConClass = 0;
	int storedOffMeshConCount = 0;
	int offMeshConLinkCount = 0;
	
	if (params->offMeshConCount > 0)
	{
		offMeshConClass = (unsigned char*)dtAlloc(sizeof(unsigned char)*params->offMeshConCount*2, DT_ALLOC_TEMP);
		if (!offMeshConClass)
			return false;

		
		float hmin = 3.402823466e+38F;
		float hmax = -3.402823466e+38F;
		
		if (params->detailVerts && params->detailVertsCount)
		{
			for (int i = 0; i < params->detailVertsCount; ++i)
			{
				const float h = params->detailVerts[i*3+1];
				hmin = dtMin(hmin,h);
				hmax = dtMax(hmax,h);
			}
		}
		else
		{
			for (int i = 0; i < params->vertCount; ++i)
			{
				const unsigned short* iv = &params->verts[i*3];
				const float h = params->bmin[1] + iv[1] * params->ch;
				hmin = dtMin(hmin,h);
				hmax = dtMax(hmax,h);
			}
		}
		hmin -= params->walkableClimb;
		hmax += params->walkableClimb;
		float bmin[3], bmax[3];
		dtVcopy(bmin, params->bmin);
		dtVcopy(bmax, params->bmax);
		bmin[1] = hmin;
		bmax[1] = hmax;

		for (int i = 0; i < params->offMeshConCount; ++i)
		{
			const float* p0 = &params->offMeshConVerts[(i*2+0)*3];
			const float* p1 = &params->offMeshConVerts[(i*2+1)*3];
			offMeshConClass[i*2+0] = classifyOffMeshPoint(p0, bmin, bmax);
			offMeshConClass[i*2+1] = classifyOffMeshPoint(p1, bmin, bmax);

			
			if (offMeshConClass[i*2+0] == 0xff)
			{
				if (p0[1] < bmin[1] || p0[1] > bmax[1])
					offMeshConClass[i*2+0] = 0;
			}

			
			if (offMeshConClass[i*2+0] == 0xff)
				offMeshConLinkCount++;
			if (offMeshConClass[i*2+1] == 0xff)
				offMeshConLinkCount++;

			if (offMeshConClass[i*2+0] == 0xff)
				storedOffMeshConCount++;
		}
	}
	
	
	const int totPolyCount = params->polyCount + storedOffMeshConCount;
	const int totVertCount = params->vertCount + storedOffMeshConCount*2;
	
	
	int edgeCount = 0;
	int portalCount = 0;
	for (int i = 0; i < params->polyCount; ++i)
	{
		const unsigned short* p = &params->polys[i*2*nvp];
		for (int j = 0; j < nvp; ++j)
		{
			if (p[j] == MESH_NULL_IDX) break;
			edgeCount++;
			
			if (p[nvp+j] & 0x8000)
			{
				unsigned short dir = p[nvp+j] & 0xf;
				if (dir != 0xf)
					portalCount++;
			}
		}
	}

	const int maxLinkCount = edgeCount + portalCount*2 + offMeshConLinkCount*2;
	
	
	int uniqueDetailVertCount = 0;
	int detailTriCount = 0;
	if (params->detailMeshes)
	{
		
		detailTriCount = params->detailTriCount;
		for (int i = 0; i < params->polyCount; ++i)
		{
			const unsigned short* p = &params->polys[i*nvp*2];
			int ndv = params->detailMeshes[i*4+1];
			int nv = 0;
			for (int j = 0; j < nvp; ++j)
			{
				if (p[j] == MESH_NULL_IDX) break;
				nv++;
			}
			ndv -= nv;
			uniqueDetailVertCount += ndv;
		}
	}
	else
	{
		
		uniqueDetailVertCount = 0; 
		detailTriCount = 0;
		for (int i = 0; i < params->polyCount; ++i)
		{
			const unsigned short* p = &params->polys[i*nvp*2];
			int nv = 0;
			for (int j = 0; j < nvp; ++j)
			{
				if (p[j] == MESH_NULL_IDX) break;
				nv++;
			}
			detailTriCount += nv-2;
		}
	}
	
	
	const int headerSize = dtAlign4(sizeof(dtMeshHeader));
	const int vertsSize = dtAlign4(sizeof(float)*3*totVertCount);
	const int polysSize = dtAlign4(sizeof(dtPoly)*totPolyCount);
	const int linksSize = dtAlign4(sizeof(dtLink)*maxLinkCount);
	const int detailMeshesSize = dtAlign4(sizeof(dtPolyDetail)*params->polyCount);
	const int detailVertsSize = dtAlign4(sizeof(float)*3*uniqueDetailVertCount);
	const int detailTrisSize = dtAlign4(sizeof(unsigned char)*4*detailTriCount);
	const int bvTreeSize = params->buildBvTree ? dtAlign4(sizeof(dtBVNode)*params->polyCount*2) : 0;
	const int offMeshConsSize = dtAlign4(sizeof(dtOffMeshConnection)*storedOffMeshConCount);
	
	const int dataSize = headerSize + vertsSize + polysSize + linksSize +
						 detailMeshesSize + detailVertsSize + detailTrisSize +
						 bvTreeSize + offMeshConsSize;
						 
	unsigned char* data = (unsigned char*)dtAlloc(sizeof(unsigned char)*dataSize, DT_ALLOC_PERM);
	if (!data)
	{
		dtFree(offMeshConClass);
		return false;
	}
	memset(data, 0, dataSize);
	
	unsigned char* d = data;
	dtMeshHeader* header = (dtMeshHeader*)d; d += headerSize;
	float* navVerts = (float*)d; d += vertsSize;
	dtPoly* navPolys = (dtPoly*)d; d += polysSize;
	d += linksSize;
	dtPolyDetail* navDMeshes = (dtPolyDetail*)d; d += detailMeshesSize;
	float* navDVerts = (float*)d; d += detailVertsSize;
	unsigned char* navDTris = (unsigned char*)d; d += detailTrisSize;
	dtBVNode* navBvtree = (dtBVNode*)d; d += bvTreeSize;
	dtOffMeshConnection* offMeshCons = (dtOffMeshConnection*)d; d += offMeshConsSize;
	
	
	
	header->magic = DT_NAVMESH_MAGIC;
	header->version = DT_NAVMESH_VERSION;
	header->x = params->tileX;
	header->y = params->tileY;
	header->layer = params->tileLayer;
	header->userId = params->userId;
	header->polyCount = totPolyCount;
	header->vertCount = totVertCount;
	header->maxLinkCount = maxLinkCount;
	dtVcopy(header->bmin, params->bmin);
	dtVcopy(header->bmax, params->bmax);
	header->detailMeshCount = params->polyCount;
	header->detailVertCount = uniqueDetailVertCount;
	header->detailTriCount = detailTriCount;
	header->bvQuantFactor = 1.0f / params->cs;
	header->offMeshBase = params->polyCount;
	header->walkableHeight = params->walkableHeight;
	header->walkableRadius = params->walkableRadius;
	header->walkableClimb = params->walkableClimb;
	header->offMeshConCount = storedOffMeshConCount;
	header->bvNodeCount = params->buildBvTree ? params->polyCount*2 : 0;
	
	const int offMeshVertsBase = params->vertCount;
	const int offMeshPolyBase = params->polyCount;
	
	
	
	for (int i = 0; i < params->vertCount; ++i)
	{
		const unsigned short* iv = &params->verts[i*3];
		float* v = &navVerts[i*3];
		v[0] = params->bmin[0] + iv[0] * params->cs;
		v[1] = params->bmin[1] + iv[1] * params->ch;
		v[2] = params->bmin[2] + iv[2] * params->cs;
	}
	
	int n = 0;
	for (int i = 0; i < params->offMeshConCount; ++i)
	{
		
		if (offMeshConClass[i*2+0] == 0xff)
		{
			const float* linkv = &params->offMeshConVerts[i*2*3];
			float* v = &navVerts[(offMeshVertsBase + n*2)*3];
			dtVcopy(&v[0], &linkv[0]);
			dtVcopy(&v[3], &linkv[3]);
			n++;
		}
	}
	
	
	
	const unsigned short* src = params->polys;
	for (int i = 0; i < params->polyCount; ++i)
	{
		dtPoly* p = &navPolys[i];
		p->vertCount = 0;
		p->flags = params->polyFlags[i];
		p->setArea(params->polyAreas[i]);
		p->setType(DT_POLYTYPE_GROUND);
		for (int j = 0; j < nvp; ++j)
		{
			if (src[j] == MESH_NULL_IDX) break;
			p->verts[j] = src[j];
			if (src[nvp+j] & 0x8000)
			{
				
				unsigned short dir = src[nvp+j] & 0xf;
				if (dir == 0xf) 
					p->neis[j] = 0;
				else if (dir == 0) 
					p->neis[j] = DT_EXT_LINK | 4;
				else if (dir == 1) 
					p->neis[j] = DT_EXT_LINK | 2;
				else if (dir == 2) 
					p->neis[j] = DT_EXT_LINK | 0;
				else if (dir == 3) 
					p->neis[j] = DT_EXT_LINK | 6;
			}
			else
			{
				
				p->neis[j] = src[nvp+j]+1;
			}
			
			p->vertCount++;
		}
		src += nvp*2;
	}
	
	n = 0;
	for (int i = 0; i < params->offMeshConCount; ++i)
	{
		
		if (offMeshConClass[i*2+0] == 0xff)
		{
			dtPoly* p = &navPolys[offMeshPolyBase+n];
			p->vertCount = 2;
			p->verts[0] = (unsigned short)(offMeshVertsBase + n*2+0);
			p->verts[1] = (unsigned short)(offMeshVertsBase + n*2+1);
			p->flags = params->offMeshConFlags[i];
			p->setArea(params->offMeshConAreas[i]);
			p->setType(DT_POLYTYPE_OFFMESH_CONNECTION);
			n++;
		}
	}

	
	
	
	if (params->detailMeshes)
	{
		unsigned short vbase = 0;
		for (int i = 0; i < params->polyCount; ++i)
		{
			dtPolyDetail& dtl = navDMeshes[i];
			const int vb = (int)params->detailMeshes[i*4+0];
			const int ndv = (int)params->detailMeshes[i*4+1];
			const int nv = navPolys[i].vertCount;
			dtl.vertBase = (unsigned int)vbase;
			dtl.vertCount = (unsigned char)(ndv-nv);
			dtl.triBase = (unsigned int)params->detailMeshes[i*4+2];
			dtl.triCount = (unsigned char)params->detailMeshes[i*4+3];
			
			if (ndv-nv)
			{
				memcpy(&navDVerts[vbase*3], &params->detailVerts[(vb+nv)*3], sizeof(float)*3*(ndv-nv));
				vbase += (unsigned short)(ndv-nv);
			}
		}
		
		memcpy(navDTris, params->detailTris, sizeof(unsigned char)*4*params->detailTriCount);
	}
	else
	{
		
		int tbase = 0;
		for (int i = 0; i < params->polyCount; ++i)
		{
			dtPolyDetail& dtl = navDMeshes[i];
			const int nv = navPolys[i].vertCount;
			dtl.vertBase = 0;
			dtl.vertCount = 0;
			dtl.triBase = (unsigned int)tbase;
			dtl.triCount = (unsigned char)(nv-2);
			
			for (int j = 2; j < nv; ++j)
			{
				unsigned char* t = &navDTris[tbase*4];
				t[0] = 0;
				t[1] = (unsigned char)(j-1);
				t[2] = (unsigned char)j;
				
				t[3] = (1<<2);
				if (j == 2) t[3] |= (1<<0);
				if (j == nv-1) t[3] |= (1<<4);
				tbase++;
			}
		}
	}

	
	
	if (params->buildBvTree)
	{
		createBVTree(params->verts, params->vertCount, params->polys, params->polyCount,
					 nvp, params->cs, params->ch, params->polyCount*2, navBvtree);
	}
	
	
	n = 0;
	for (int i = 0; i < params->offMeshConCount; ++i)
	{
		
		if (offMeshConClass[i*2+0] == 0xff)
		{
			dtOffMeshConnection* con = &offMeshCons[n];
			con->poly = (unsigned short)(offMeshPolyBase + n);
			
			const float* endPts = &params->offMeshConVerts[i*2*3];
			dtVcopy(&con->pos[0], &endPts[0]);
			dtVcopy(&con->pos[3], &endPts[3]);
			con->rad = params->offMeshConRad[i];
			con->flags = params->offMeshConDir[i] ? DT_OFFMESH_CON_BIDIR : 0;
			con->side = offMeshConClass[i*2+1];
			if (params->offMeshConUserID)
				con->userId = params->offMeshConUserID[i];
			n++;
		}
	}
		
	dtFree(offMeshConClass);
	
	*outData = data;
	*outDataSize = dataSize;
	
	return true;
}

bool dtNavMeshHeaderSwapEndian(unsigned char* data, const int )
{
	dtMeshHeader* header = (dtMeshHeader*)data;
	
	int swappedMagic = DT_NAVMESH_MAGIC;
	int swappedVersion = DT_NAVMESH_VERSION;
	dtSwapEndian(&swappedMagic);
	dtSwapEndian(&swappedVersion);
	
	if ((header->magic != DT_NAVMESH_MAGIC || header->version != DT_NAVMESH_VERSION) &&
		(header->magic != swappedMagic || header->version != swappedVersion))
	{
		return false;
	}
		
	dtSwapEndian(&header->magic);
	dtSwapEndian(&header->version);
	dtSwapEndian(&header->x);
	dtSwapEndian(&header->y);
	dtSwapEndian(&header->layer);
	dtSwapEndian(&header->userId);
	dtSwapEndian(&header->polyCount);
	dtSwapEndian(&header->vertCount);
	dtSwapEndian(&header->maxLinkCount);
	dtSwapEndian(&header->detailMeshCount);
	dtSwapEndian(&header->detailVertCount);
	dtSwapEndian(&header->detailTriCount);
	dtSwapEndian(&header->bvNodeCount);
	dtSwapEndian(&header->offMeshConCount);
	dtSwapEndian(&header->offMeshBase);
	dtSwapEndian(&header->walkableHeight);
	dtSwapEndian(&header->walkableRadius);
	dtSwapEndian(&header->walkableClimb);
	dtSwapEndian(&header->bmin[0]);
	dtSwapEndian(&header->bmin[1]);
	dtSwapEndian(&header->bmin[2]);
	dtSwapEndian(&header->bmax[0]);
	dtSwapEndian(&header->bmax[1]);
	dtSwapEndian(&header->bmax[2]);
	dtSwapEndian(&header->bvQuantFactor);

	

	return true;
}







bool dtNavMeshDataSwapEndian(unsigned char* data, const int )
{
	
	dtMeshHeader* header = (dtMeshHeader*)data;
	if (header->magic != DT_NAVMESH_MAGIC)
		return false;
	if (header->version != DT_NAVMESH_VERSION)
		return false;
	
	
	const int headerSize = dtAlign4(sizeof(dtMeshHeader));
	const int vertsSize = dtAlign4(sizeof(float)*3*header->vertCount);
	const int polysSize = dtAlign4(sizeof(dtPoly)*header->polyCount);
	const int linksSize = dtAlign4(sizeof(dtLink)*(header->maxLinkCount));
	const int detailMeshesSize = dtAlign4(sizeof(dtPolyDetail)*header->detailMeshCount);
	const int detailVertsSize = dtAlign4(sizeof(float)*3*header->detailVertCount);
	const int detailTrisSize = dtAlign4(sizeof(unsigned char)*4*header->detailTriCount);
	const int bvtreeSize = dtAlign4(sizeof(dtBVNode)*header->bvNodeCount);
	const int offMeshLinksSize = dtAlign4(sizeof(dtOffMeshConnection)*header->offMeshConCount);
	
	unsigned char* d = data + headerSize;
	float* verts = (float*)d; d += vertsSize;
	dtPoly* polys = (dtPoly*)d; d += polysSize;
	 d += linksSize;
	dtPolyDetail* detailMeshes = (dtPolyDetail*)d; d += detailMeshesSize;
	float* detailVerts = (float*)d; d += detailVertsSize;
	 d += detailTrisSize;
	dtBVNode* bvTree = (dtBVNode*)d; d += bvtreeSize;
	dtOffMeshConnection* offMeshCons = (dtOffMeshConnection*)d; d += offMeshLinksSize;
	
	
	for (int i = 0; i < header->vertCount*3; ++i)
	{
		dtSwapEndian(&verts[i]);
	}

	
	for (int i = 0; i < header->polyCount; ++i)
	{
		dtPoly* p = &polys[i];
		
		for (int j = 0; j < DT_VERTS_PER_POLYGON; ++j)
		{
			dtSwapEndian(&p->verts[j]);
			dtSwapEndian(&p->neis[j]);
		}
		dtSwapEndian(&p->flags);
	}

	

	
	for (int i = 0; i < header->detailMeshCount; ++i)
	{
		dtPolyDetail* pd = &detailMeshes[i];
		dtSwapEndian(&pd->vertBase);
		dtSwapEndian(&pd->triBase);
	}
	
	
	for (int i = 0; i < header->detailVertCount*3; ++i)
	{
		dtSwapEndian(&detailVerts[i]);
	}

	
	for (int i = 0; i < header->bvNodeCount; ++i)
	{
		dtBVNode* node = &bvTree[i];
		for (int j = 0; j < 3; ++j)
		{
			dtSwapEndian(&node->bmin[j]);
			dtSwapEndian(&node->bmax[j]);
		}
		dtSwapEndian(&node->i);
	}

	
	for (int i = 0; i < header->offMeshConCount; ++i)
	{
		dtOffMeshConnection* con = &offMeshCons[i];
		for (int j = 0; j < 6; ++j)
			dtSwapEndian(&con->pos[j]);
		dtSwapEndian(&con->rad);
		dtSwapEndian(&con->poly);
	}
	
	return true;
}
