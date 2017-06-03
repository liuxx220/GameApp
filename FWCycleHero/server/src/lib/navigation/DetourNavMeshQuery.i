





















































































































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




























enum dtNodeFlags
{
	DT_NODE_OPEN = 0x01,
	DT_NODE_CLOSED = 0x02,
};

typedef unsigned short dtNodeIndex;
static const dtNodeIndex DT_NULL_IDX = (dtNodeIndex)~0;

struct dtNode
{
	float pos[3];				
	float cost;					
	float total;				
	unsigned int pidx : 30;		
	unsigned int flags : 2;		
	dtPolyRef id;				
};


class dtNodePool
{
public:
	dtNodePool(int maxNodes, int hashSize);
	~dtNodePool();
	inline void operator=(const dtNodePool&) {}
	void clear();
	dtNode* getNode(dtPolyRef id);
	dtNode* findNode(dtPolyRef id);

	inline unsigned int getNodeIdx(const dtNode* node) const
	{
		if (!node) return 0;
		return (unsigned int)(node - m_nodes)+1;
	}

	inline dtNode* getNodeAtIdx(unsigned int idx)
	{
		if (!idx) return 0;
		return &m_nodes[idx-1];
	}

	inline const dtNode* getNodeAtIdx(unsigned int idx) const
	{
		if (!idx) return 0;
		return &m_nodes[idx-1];
	}
	
	inline int getMemUsed() const
	{
		return sizeof(*this) +
			sizeof(dtNode)*m_maxNodes +
			sizeof(dtNodeIndex)*m_maxNodes +
			sizeof(dtNodeIndex)*m_hashSize;
	}
	
	inline int getMaxNodes() const { return m_maxNodes; }
	
	inline int getHashSize() const { return m_hashSize; }
	inline dtNodeIndex getFirst(int bucket) const { return m_first[bucket]; }
	inline dtNodeIndex getNext(int i) const { return m_next[i]; }
	
private:
	
	dtNode* m_nodes;
	dtNodeIndex* m_first;
	dtNodeIndex* m_next;
	const int m_maxNodes;
	const int m_hashSize;
	int m_nodeCount;
};

class dtNodeQueue
{
public:
	dtNodeQueue(int n);
	~dtNodeQueue();
	inline void operator=(dtNodeQueue&) {}
	
	inline void clear()
	{
		m_size = 0;
	}
	
	inline dtNode* top()
	{
		return m_heap[0];
	}
	
	inline dtNode* pop()
	{
		dtNode* result = m_heap[0];
		m_size--;
		trickleDown(0, m_heap[m_size]);
		return result;
	}
	
	inline void push(dtNode* node)
	{
		m_size++;
		bubbleUp(m_size-1, node);
	}
	
	inline void modify(dtNode* node)
	{
		for (int i = 0; i < m_size; ++i)
		{
			if (m_heap[i] == node)
			{
				bubbleUp(i, node);
				return;
			}
		}
	}
	
	inline bool empty() const { return m_size == 0; }
	
	inline int getMemUsed() const
	{
		return sizeof(*this) +
		sizeof(dtNode*)*(m_capacity+1);
	}
	
	inline int getCapacity() const { return m_capacity; }
	
private:
	void bubbleUp(int i, dtNode* node);
	void trickleDown(int i, dtNode* node);
	
	dtNode** m_heap;
	const int m_capacity;
	int m_size;
};		










































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













#pragma once





#pragma once






#pragma once



















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




 void __cdecl perror(  const char * _ErrMsg);


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


 void __cdecl _wperror(  const wchar_t * _ErrMsg);



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




#pragma once






#pragma once






 


















































































  



















































































 





















































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































#pragma pack(push,8)








































































		








		


		




		

 
  

 



 
  
 



 
  
 










































	
	






		
			
		


	

	
	




		
			
		


	

	
	
		
	












	
		#pragma detect_mismatch("_MSC_VER", "1800")
	

	
		#pragma detect_mismatch("_ITERATOR_DEBUG_LEVEL", "2")
	

	
		


			#pragma detect_mismatch("RuntimeLibrary", "MTd_StaticDebug")
		




	










	


		
	
































 


 
 

 









 









 









 

 









 









 




 





 









































#pragma once




















    
    








    

    






    


#pragma comment(lib, "libcpmt" "d" "")


























 















 
  


   
  
 

 












 
















 
  


   
  
 

 
  


   
  
 


 
  







   



    
   

  
 

 



 
  


   


     
   
  
 

 


























  
   
  
 

		

 
  
  
  




  
  
  

  







   
   
   
  

  
  
  
  

 














 
namespace std {
typedef bool _Bool;
}
 

		





		






typedef __int64 _Longlong;
typedef unsigned __int64 _ULonglong;

		







 


		
 

 
  
typedef unsigned short char16_t;
typedef unsigned int char32_t;
 
 

		
		






 
namespace std {
enum _Uninitialized
	{	
	_Noinit
	};

		

#pragma warning(push)
#pragma warning(disable:4412)
class  _Lockit
	{	
public:
 

  

















	__thiscall _Lockit();	
	explicit __thiscall _Lockit(int);	
	__thiscall ~_Lockit() throw ();	
  

    static  void __cdecl _Lockit_ctor(int);
    static  void __cdecl _Lockit_dtor(int);

private:
    static  void __cdecl _Lockit_ctor(_Lockit *);
    static  void __cdecl _Lockit_ctor(_Lockit *, int);
    static  void __cdecl _Lockit_dtor(_Lockit *);

public:
	 _Lockit(const _Lockit&) = delete;
	_Lockit&  operator=(const _Lockit&) = delete;

private:
	int _Locktype;

  












	};

 



































































  



  


  



  


  
 

class  _Init_locks
	{	
public:
 
      











    __thiscall _Init_locks();
	__thiscall ~_Init_locks() throw ();
  

private:
    static  void __cdecl _Init_locks_ctor(_Init_locks *);
    static  void __cdecl _Init_locks_dtor(_Init_locks *);

 








	};

#pragma warning(pop)
}
 





		

 void __cdecl _Atexit(void (__cdecl *)(void));

typedef int _Mbstatet;
typedef unsigned long _Uint32t;




 
 

 
 #pragma pack(pop)















 














#pragma once















































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































extern "C" {












namespace std { typedef decltype(__nullptr) nullptr_t; }
using ::std::nullptr_t;































 extern unsigned long  __cdecl __threadid(void);

 extern uintptr_t __cdecl __threadhandle(void);


}






 
namespace std {
using :: ptrdiff_t; using :: size_t;
}
 

 
namespace std {
typedef double max_align_t;	
}
 









#pragma once





 #pragma pack(push,8)
 #pragma warning(push,3)
 
 

namespace std {
		
template<class _Elem>
	class initializer_list
	{	
public:
	typedef _Elem value_type;
	typedef const _Elem& reference;
	typedef const _Elem& const_reference;
	typedef size_t size_type;

	typedef const _Elem* iterator;
	typedef const _Elem* const_iterator;

	initializer_list() throw ()
		: _First(0), _Last(0)
		{	
		}

	initializer_list(const _Elem *_First_arg,
		const _Elem *_Last_arg) throw ()
		: _First(_First_arg), _Last(_Last_arg)
		{	
		}

	const _Elem *begin() const throw ()
		{	
		return (_First);
		}

	const _Elem *end() const throw ()
		{	
		return (_Last);
		}

	size_t size() const throw ()
		{	
		return ((size_t)(_Last - _First));
		}

private:
	const _Elem *_First;
	const _Elem *_Last;
	};

		
template<class _Elem> inline
	const _Elem *begin(initializer_list<_Elem> _Ilist) throw ()
	{	
	return (_Ilist.begin());
	}

		
template<class _Elem> inline
	const _Elem *end(initializer_list<_Elem> _Ilist) throw ()
	{	
	return (_Ilist.end());
	}
}

 
 #pragma warning(pop)
 #pragma pack(pop)











#pragma once





 #pragma pack(push,8)
 #pragma warning(push,3)
 
 

namespace std {
	
template<class _T1,
	class _Ret>
	struct unary_function;

	
template<class _T1,
	class _T2,
	class _Ret>
	struct binary_function;

	
struct _Nil
	{	
	};
static _Nil _Nil_obj;

	
template<class _Ty,
	_Ty _Val>
	struct integral_constant
	{	
	static const _Ty value = _Val;

	typedef _Ty value_type;
	typedef integral_constant<_Ty, _Val> type;

	operator value_type() const
		{	
		return (value);
		}
	};

typedef integral_constant<bool, true> true_type;
typedef integral_constant<bool, false> false_type;

	
template<bool>
	struct _Cat_base
		: false_type
	{	
	};

template<>
	struct _Cat_base<true>
		: true_type
	{	
	};

	
template<bool _Test,
	class _Ty = void>
	struct enable_if
	{	
	};

template<class _Ty>
	struct enable_if<true, _Ty>
	{	
	typedef _Ty type;
	};

	
template<bool _Test,
	class _Ty1,
	class _Ty2>
	struct conditional
	{	
	typedef _Ty2 type;
	};

template<class _Ty1,
	class _Ty2>
	struct conditional<true, _Ty1, _Ty2>
	{	
	typedef _Ty1 type;
	};

	
template<class _Ty1, class _Ty2>
	struct is_same
		: false_type
	{	
	};

template<class _Ty1>
	struct is_same<_Ty1, _Ty1>
		: true_type
	{	
	};

	
template<class _Ty>
	struct remove_const
	{	
	typedef _Ty type;
	};

template<class _Ty>
	struct remove_const<const _Ty>
	{	
	typedef _Ty type;
	};

template<class _Ty>
	struct remove_const<const _Ty[]>
	{	
	typedef _Ty type[];
	};

template<class _Ty, unsigned int _Nx>
	struct remove_const<const _Ty[_Nx]>
	{	
	typedef _Ty type[_Nx];
	};

	
template<class _Ty>
	struct remove_volatile
	{	
	typedef _Ty type;
	};

template<class _Ty>
	struct remove_volatile<volatile _Ty>
	{	
	typedef _Ty type;
	};

template<class _Ty>
	struct remove_volatile<volatile _Ty[]>
	{	
	typedef _Ty type[];
	};

template<class _Ty, unsigned int _Nx>
	struct remove_volatile<volatile _Ty[_Nx]>
	{	
	typedef _Ty type[_Nx];
	};

	
template<class _Ty>
	struct remove_cv
	{	
	typedef typename remove_const<typename remove_volatile<_Ty>::type>::type
		type;
	};

	
template<class _Ty>
	struct _Is_integral
		: false_type
	{	
	};

template<>
	struct _Is_integral<bool>
		: true_type
	{	
	};

template<>
	struct _Is_integral<char>
		: true_type
	{	
	};

template<>
	struct _Is_integral<unsigned char>
		: true_type
	{	
	};

template<>
	struct _Is_integral<signed char>
		: true_type
	{	
	};

 
template<>
	struct _Is_integral<wchar_t>
		: true_type
	{	
	};
 

template<>
	struct _Is_integral<unsigned short>
		: true_type
	{	
	};

template<>
	struct _Is_integral<signed short>
		: true_type
	{	
	};

template<>
	struct _Is_integral<unsigned int>
		: true_type
	{	
	};

template<>
	struct _Is_integral<signed int>
		: true_type
	{	
	};

template<>
	struct _Is_integral<unsigned long>
		: true_type
	{	
	};

template<>
	struct _Is_integral<signed long>
		: true_type
	{	
	};

 













 
template<>
	struct _Is_integral<__int64>
		: true_type
	{	
	};

template<>
	struct _Is_integral<unsigned __int64>
		: true_type
	{	
	};
 

	
template<class _Ty>
	struct is_integral
		: _Is_integral<typename remove_cv<_Ty>::type>
	{	
	};

	
template<class _Ty>
	struct _Is_floating_point
		: false_type
	{	
	};

template<>
	struct _Is_floating_point<float>
		: true_type
	{	
	};

template<>
	struct _Is_floating_point<double>
		: true_type
	{	
	};

template<>
	struct _Is_floating_point<long double>
		: true_type
	{	
	};

	
template<class _Ty>
	struct is_floating_point
		: _Is_floating_point<typename remove_cv<_Ty>::type>
	{	
	};

template<class _Ty>
	struct _Is_numeric
		: _Cat_base<is_integral<_Ty>::value
			|| is_floating_point<_Ty>::value>
	{	
	};

	
template<class _Ty>
	struct remove_reference
	{	
	typedef _Ty type;
	};

template<class _Ty>
	struct remove_reference<_Ty&>
	{	
	typedef _Ty type;
	};

template<class _Ty>
	struct remove_reference<_Ty&&>
	{	
	typedef _Ty type;
	};

	
template<class _Tgt,
	class _Src>
	struct _Copy_cv
	{	
	typedef typename remove_reference<_Tgt>::type _Tgtx;
	typedef _Tgtx& type;
	};

template<class _Tgt,
	class _Src>
	struct _Copy_cv<_Tgt, const _Src>
	{	
	typedef typename remove_reference<_Tgt>::type _Tgtx;
	typedef const _Tgtx& type;
	};

template<class _Tgt,
	class _Src>
	struct _Copy_cv<_Tgt, volatile _Src>
	{	
	typedef typename remove_reference<_Tgt>::type _Tgtx;
	typedef volatile _Tgtx& type;
	};

template<class _Tgt,
	class _Src>
	struct _Copy_cv<_Tgt, const volatile _Src>
	{	
	typedef typename remove_reference<_Tgt>::type _Tgtx;
	typedef const volatile _Tgtx& type;
	};

template<class _Tgt,
	class _Src>
	struct _Copy_cv<_Tgt, _Src&>
	{	
	typedef typename _Copy_cv<_Tgt, _Src>::type type;
	};

	
struct _Wrap_int
	{	
	_Wrap_int(int)
		{	
		}
	};

template<class _Ty>
	struct _Identity
	{	
	typedef _Ty type;
	};


































		
template<class _Ty>
	struct _Has_result_type
		{ template<class _Uty> static auto _Fn(int, _Identity<typename _Uty::result_type> * = 0, _Identity<typename _Uty::result_type> * = 0, _Identity<typename _Uty::result_type> * = 0) -> true_type; template<class _Uty> static auto _Fn(_Wrap_int) -> false_type; typedef decltype(_Fn<_Ty>(0)) type; };
}
 
 #pragma warning(pop)
 #pragma pack(pop)









 #pragma pack(push,8)
 #pragma warning(push,3)
 
 

 
  
  
  
 

namespace std {
		
 
 
 

 
 
 
 
 

 
 

  
  

  












   
   
  

 






















		


		
 
 

		
template<class _Ty> inline
	_Ty *addressof(_Ty& _Val) throw ()
	{	
	return (reinterpret_cast<_Ty *>(
		(&const_cast<char&>(
		reinterpret_cast<const volatile char&>(_Val)))));
	}

		

template<bool,
	class _Ty1,
	class _Ty2>
	struct _If
	{	
	typedef _Ty2 type;
	};

template<class _Ty1,
	class _Ty2>
	struct _If<true, _Ty1, _Ty2>
	{	
	typedef _Ty1 type;
	};

template<class _Ty>
	struct _Always_false
	{	
	static const bool value = false;
	};

		
		
template<class _Arg,
	class _Result>
	struct unary_function
	{	
	typedef _Arg argument_type;
	typedef _Result result_type;
	};

		
template<class _Arg1,
	class _Arg2,
	class _Result>
	struct binary_function
	{	
	typedef _Arg1 first_argument_type;
	typedef _Arg2 second_argument_type;
	typedef _Result result_type;
	};

		
template<class _Ty = void>
	struct plus
		: public binary_function<_Ty, _Ty, _Ty>
	{	
	_Ty operator()(const _Ty& _Left, const _Ty& _Right) const
		{	
		return (_Left + _Right);
		}
	};

		
template<class _Ty = void>
	struct minus
		: public binary_function<_Ty, _Ty, _Ty>
	{	
	_Ty operator()(const _Ty& _Left, const _Ty& _Right) const
		{	
		return (_Left - _Right);
		}
	};

		
template<class _Ty = void>
	struct multiplies
		: public binary_function<_Ty, _Ty, _Ty>
	{	
	_Ty operator()(const _Ty& _Left, const _Ty& _Right) const
		{	
		return (_Left * _Right);
		}
	};

		
template<class _Ty = void>
	struct equal_to
		: public binary_function<_Ty, _Ty, bool>
	{	
	bool operator()(const _Ty& _Left, const _Ty& _Right) const
		{	
		return (_Left == _Right);
		}
	};

		
template<class _Ty = void>
	struct less
		: public binary_function<_Ty, _Ty, bool>
	{	
	bool operator()(const _Ty& _Left, const _Ty& _Right) const
		{	
		return (_Left < _Right);
		}
	};

		
template<>
	struct plus<void>
	{	
	template<class _Ty1,
		class _Ty2>
		auto operator()(_Ty1&& _Left, _Ty2&& _Right) const
		-> decltype(static_cast<_Ty1&&>(_Left)
			+ static_cast<_Ty2&&>(_Right))
		{	
		return (static_cast<_Ty1&&>(_Left)
			+ static_cast<_Ty2&&>(_Right));
		}
	};

		
template<>
	struct minus<void>
	{	
	template<class _Ty1,
		class _Ty2>
		auto operator()(_Ty1&& _Left, _Ty2&& _Right) const
		-> decltype(static_cast<_Ty1&&>(_Left)
			- static_cast<_Ty2&&>(_Right))
		{	
		return (static_cast<_Ty1&&>(_Left)
			- static_cast<_Ty2&&>(_Right));
		}
	};

		
template<>
	struct multiplies<void>
	{	
	template<class _Ty1,
		class _Ty2>
		auto operator()(_Ty1&& _Left, _Ty2&& _Right) const
		-> decltype(static_cast<_Ty1&&>(_Left)
			* static_cast<_Ty2&&>(_Right))
		{	
		return (static_cast<_Ty1&&>(_Left)
			* static_cast<_Ty2&&>(_Right));
		}
	};

		
template<>
	struct equal_to<void>
	{	
	template<class _Ty1,
		class _Ty2>
		auto operator()(_Ty1&& _Left, _Ty2&& _Right) const
		-> decltype(static_cast<_Ty1&&>(_Left)
			== static_cast<_Ty2&&>(_Right))
		{	
		return (static_cast<_Ty1&&>(_Left)
			== static_cast<_Ty2&&>(_Right));
		}
	};

		
template<>
	struct less<void>
	{	
	template<class _Ty1,
		class _Ty2>
		auto operator()(_Ty1&& _Left, _Ty2&& _Right) const
		-> decltype(static_cast<_Ty1&&>(_Left)
			< static_cast<_Ty2&&>(_Right))
		{	
		return (static_cast<_Ty1&&>(_Left)
			< static_cast<_Ty2&&>(_Right));
		}
	};


}



namespace std {
	
inline size_t _Hash_seq(const unsigned char *_First, size_t _Count)
	{	
 





	static_assert(sizeof(size_t) == 4, "This code is for 32-bit size_t.");
	const size_t _FNV_offset_basis = 2166136261U;
	const size_t _FNV_prime = 16777619U;
 

	size_t _Val = _FNV_offset_basis;
	for (size_t _Next = 0; _Next < _Count; ++_Next)
		{	
		_Val ^= (size_t)_First[_Next];
		_Val *= _FNV_prime;
		}

 




	static_assert(sizeof(size_t) == 4, "This code is for 32-bit size_t.");
 

	return (_Val);
	}

	
template<class _Kty>
	struct _Bitwise_hash
		: public unary_function<_Kty, size_t>
	{	
	size_t operator()(const _Kty& _Keyval) const
		{	
		return (_Hash_seq((const unsigned char *)&_Keyval, sizeof (_Kty)));
		}
	};

	
template<class _Kty>
	struct hash
		: public _Bitwise_hash<_Kty>
	{	
	static const bool _Value = __is_enum(_Kty);
	static_assert(_Value,
		"The C++ Standard doesn't provide a hash for this type.");
	};
template<>
	struct hash<bool>
		: public _Bitwise_hash<bool>
	{	
	};

template<>
	struct hash<char>
		: public _Bitwise_hash<char>
	{	
	};

template<>
	struct hash<signed char>
		: public _Bitwise_hash<signed char>
	{	
	};

template<>
	struct hash<unsigned char>
		: public _Bitwise_hash<unsigned char>
	{	
	};

 













 
template<>
	struct hash<wchar_t>
		: public _Bitwise_hash<wchar_t>
	{	
	};
 

template<>
	struct hash<short>
		: public _Bitwise_hash<short>
	{	
	};

template<>
	struct hash<unsigned short>
		: public _Bitwise_hash<unsigned short>
	{	
	};

template<>
	struct hash<int>
		: public _Bitwise_hash<int>
	{	
	};

template<>
	struct hash<unsigned int>
		: public _Bitwise_hash<unsigned int>
	{	
	};

template<>
	struct hash<long>
		: public _Bitwise_hash<long>
	{	
	};

template<>
	struct hash<unsigned long>
		: public _Bitwise_hash<unsigned long>
	{	
	};

template<>
	struct hash<long long>
		: public _Bitwise_hash<long long>
	{	
	};

template<>
	struct hash<unsigned long long>
		: public _Bitwise_hash<unsigned long long>
	{	
	};

template<>
	struct hash<float>
		: public _Bitwise_hash<float>
	{	
	typedef float _Kty;
	typedef _Bitwise_hash<_Kty> _Mybase;

	size_t operator()(const _Kty& _Keyval) const
		{	
		return (_Mybase::operator()(
			_Keyval == 0 ? 0 : _Keyval)); 
		}
	};

template<>
	struct hash<double>
		: public _Bitwise_hash<double>
	{	
	typedef double _Kty;
	typedef _Bitwise_hash<_Kty> _Mybase;

	size_t operator()(const _Kty& _Keyval) const
		{	
		return (_Mybase::operator()(
			_Keyval == 0 ? 0 : _Keyval)); 
		}
	};

template<>
	struct hash<long double>
		: public _Bitwise_hash<long double>
	{	
	typedef long double _Kty;
	typedef _Bitwise_hash<_Kty> _Mybase;

	size_t operator()(const _Kty& _Keyval) const
		{	
		return (_Mybase::operator()(
			_Keyval == 0 ? 0 : _Keyval)); 
		}
	};

template<class _Ty>
	struct hash<_Ty *>
		: public _Bitwise_hash<_Ty *>
	{	
	};
}


namespace std {
namespace tr1 {	
using ::std:: hash;
}	
}

  
















 

  




















  

 












































 
 #pragma warning(pop)
 #pragma pack(pop)









 #pragma pack(push,8)
 #pragma warning(push,3)
 
 










namespace std {

  


  



  



}

 

 












#pragma once


















































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































#pragma pack(push,8)







typedef void (__cdecl *terminate_function)();
typedef void (__cdecl *terminate_handler)();
typedef void (__cdecl *unexpected_function)();
typedef void (__cdecl *unexpected_handler)();














struct _EXCEPTION_POINTERS;

typedef void (__cdecl *_se_translator_function)(unsigned int, struct _EXCEPTION_POINTERS*);


 __declspec(noreturn) void __cdecl terminate(void);
 __declspec(noreturn) void __cdecl unexpected(void);

 int __cdecl _is_exception_typeof(  const type_info &_Type,   struct _EXCEPTION_POINTERS * _ExceptionPtr);



 terminate_function __cdecl set_terminate(  terminate_function _NewPtFunc);
extern "C"  terminate_function __cdecl _get_terminate(void);
 unexpected_function __cdecl set_unexpected(  unexpected_function _NewPtFunc);
extern "C"  unexpected_function __cdecl _get_unexpected(void);




 _se_translator_function __cdecl _set_se_translator(  _se_translator_function _NewPtFunc);

 bool __cdecl __uncaught_exception();









#pragma pack(pop)



 














#pragma once


















































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































































#pragma pack(push,8)


extern "C" {




























typedef struct _heapinfo {
        int * _pentry;
        size_t _size;
        int _useflag;
        } _HEAPINFO;
















































































 int     __cdecl _resetstkoflw (void);




 unsigned long __cdecl _set_malloc_crt_max_wait(  unsigned long _NewValue);









       void *  __cdecl _expand(  void * _Memory,   size_t _NewSize);
   size_t  __cdecl _msize(  void * _Memory);






    void *          __cdecl _alloca(  size_t _Size);


  int     __cdecl _heapwalk(  _HEAPINFO * _EntryInfo);
 intptr_t __cdecl _get_heap_handle(void);



   int     __cdecl _heapadd(  void * _Memory,   size_t _Size);
   int     __cdecl _heapchk(void);
   int     __cdecl _heapmin(void);
 int     __cdecl _heapset(  unsigned int _Fill);
 size_t  __cdecl _heapused(size_t * _Used, size_t * _Commit);
















typedef char __static_assert_t[ (sizeof(unsigned int) <= 8) ];


#pragma warning(push)
#pragma warning(disable:6540)
__inline void *_MarkAllocaS(   void *_Ptr, unsigned int _Marker)
{
    if (_Ptr)
    {
        *((unsigned int*)_Ptr) = _Marker;
        _Ptr = (char*)_Ptr + 8;
    }
    return _Ptr;
}

__inline int _MallocaIsSizeInRange(size_t size)
{
    return size + 8 > size;
}
#pragma warning(pop)






































__pragma(warning(push))
__pragma(warning(disable: 6014))
__declspec(noalias) __inline void __cdecl _freea(    void * _Memory)
{
    unsigned int _Marker;
    if (_Memory)
    {
        _Memory = (char*)_Memory - 8;
        _Marker = *(unsigned int *)_Memory;
        if (_Marker == 0xDDDD)
        {
            free(_Memory);
        }







    }
}
__pragma(warning(pop))













}


#pragma pack(pop)



 

 



 































 namespace std {





 


class  exception
	{   
public:
	  exception();
	 explicit  exception(const char * const &);
	  exception(const char * const &, int);
	  exception(const exception&);
	 exception&  operator=(const exception&);
	 virtual  ~exception() throw ();
	 virtual const char *  what() const;

private:
	 void  _Copy_str(const char *);
	 void  _Tidy();

	const char * _Mywhat;
	bool _Mydofree;
	};

















































































using ::set_terminate; using ::terminate_handler; using ::terminate; using ::set_unexpected; using ::unexpected_handler; using ::unexpected;

typedef void (__cdecl *_Prhand)(const exception&);

 bool __cdecl uncaught_exception();


inline terminate_handler __cdecl get_terminate()
	{	
	return (_get_terminate());
	}

inline unexpected_handler __cdecl get_unexpected()
	{	
	return (_get_unexpected());
	}


}

 























































































































namespace std {




		
class bad_exception : public exception
	{	
public:
	 bad_exception(const char *_Message = "bad exception")
		throw ()
		: exception(_Message)
		{	
		}

	virtual  ~bad_exception() throw ()
		{	
		}

 







	};

		
class bad_alloc : public exception
	{	
public:
	 bad_alloc() throw ()
		: exception("bad allocation", 1)
		{	
		}

	virtual  ~bad_alloc() throw ()
		{	
		}

private:
	friend class bad_array_new_length;

	 bad_alloc(const char *_Message) throw ()
		: exception(_Message, 1)
		{	
		}

 







	};

		
class bad_array_new_length
	: public bad_alloc
	{	
public:

	bad_array_new_length() throw ()
		: bad_alloc("bad array new length")
		{	
		}
	};


}











 void __cdecl __ExceptionPtrCreate(  void* );
 void __cdecl __ExceptionPtrDestroy(  void* );
 void __cdecl __ExceptionPtrCopy(  void*,   const void* );
 void __cdecl __ExceptionPtrAssign(  void*,   const void* );
 bool __cdecl __ExceptionPtrCompare(  const void*,   const void*);
 bool __cdecl __ExceptionPtrToBool(  const void*);
 void __cdecl __ExceptionPtrSwap(  void*,   void*);

 void __cdecl __ExceptionPtrCurrentException(  void*);
 void __cdecl __ExceptionPtrRethrow(  const void*);
 void __cdecl __ExceptionPtrCopyException(  void*,   const void*,   const void*);

namespace std {

class exception_ptr
	{
public:
	exception_ptr()
		{
		__ExceptionPtrCreate(this);
		}
	exception_ptr(nullptr_t)
		{
		__ExceptionPtrCreate(this);
		}
	~exception_ptr() throw ()
		{
		__ExceptionPtrDestroy(this);
		}
	exception_ptr(const exception_ptr& _Rhs)
		{
		__ExceptionPtrCopy(this, &_Rhs);
		}
	exception_ptr& operator=(const exception_ptr& _Rhs)
		{
		__ExceptionPtrAssign(this, &_Rhs);
		return *this;
		}
	exception_ptr& operator=(nullptr_t)
		{
		exception_ptr _Ptr;
		__ExceptionPtrAssign(this, &_Ptr);
		return *this;
		}

	typedef exception_ptr _Myt;

	explicit operator bool() const throw ()
		{
		return __ExceptionPtrToBool(this);
		}

	void _RethrowException() const
		{
		__ExceptionPtrRethrow(this);
		}

	static exception_ptr _Current_exception()
		{
		exception_ptr _Retval;
		__ExceptionPtrCurrentException(&_Retval);
		return _Retval;
		}
	static exception_ptr _Copy_exception(  void* _Except,   const void* _Ptr)
		{
		exception_ptr _Retval = 0;
		if (!_Ptr)
			{
			
			return _Retval;
			}
		__ExceptionPtrCopyException(&_Retval, _Except, _Ptr);
		return _Retval;
		}
private:
	void* _Data1;
	void* _Data2;
	};

inline void swap(exception_ptr& _Lhs, exception_ptr& _Rhs)
	{
	__ExceptionPtrSwap(&_Lhs, &_Rhs);
	}

inline bool operator==(const exception_ptr& _Lhs, const exception_ptr& _Rhs)
	{
	return __ExceptionPtrCompare(&_Lhs, &_Rhs);
	}

inline bool operator==(nullptr_t, const exception_ptr& _Rhs)
	{
	return !_Rhs;
	}

inline bool operator==(const exception_ptr& _Lhs, nullptr_t)
	{
	return !_Lhs;
	}

inline bool operator!=(const exception_ptr& _Lhs, const exception_ptr& _Rhs)
	{
	return !(_Lhs == _Rhs);
	}

inline bool operator!=(nullptr_t _Lhs, const exception_ptr& _Rhs)
	{
	return !(_Lhs == _Rhs);
	}

inline bool operator!=(const exception_ptr& _Lhs, nullptr_t _Rhs)
	{
	return !(_Lhs == _Rhs);
	}

inline exception_ptr current_exception()
	{
	return exception_ptr::_Current_exception();
	}

inline void rethrow_exception(  exception_ptr _P)
	{
	_P._RethrowException();
	}

template <class _E> void *__GetExceptionInfo(_E);

template<class _E> exception_ptr make_exception_ptr(_E _Except)
	{
	return exception_ptr::_Copy_exception(::std:: addressof(_Except), __GetExceptionInfo(_Except));
	}
}







 
 #pragma warning(pop)
 #pragma pack(pop)










 #pragma pack(push,8)
 #pragma warning(push,3)
 

  










namespace std {

		
 




typedef void (__cdecl * new_handler) ();

 

 
struct nothrow_t
	{	
	};

extern const nothrow_t nothrow;	
 

		
 new_handler __cdecl set_new_handler(  new_handler)
	throw ();	

 new_handler __cdecl get_new_handler()
	throw ();	
}

		
void __cdecl operator delete(void *) throw ();
#pragma warning (suppress: 4985)
    void *__cdecl operator new(size_t _Size) throw (...);

 
  
inline void *__cdecl operator new(size_t, void *_Where) throw ()
	{	
	return (_Where);
	}

inline void __cdecl operator delete(void *, void *) throw ()
	{	
	}
 

 
  
inline void *__cdecl operator new[](size_t, void *_Where) throw ()
	{	
	return (_Where);
	}

inline void __cdecl operator delete[](void *, void *) throw ()
	{	
	}
 

void __cdecl operator delete[](void *) throw ();	

    void *__cdecl operator new[](size_t _Size)
	throw (...);	

 
  
    void *__cdecl operator new(size_t _Size, const ::std:: nothrow_t&)
	throw ();

    void *__cdecl operator new[](size_t _Size, const ::std:: nothrow_t&)
	throw ();	

void __cdecl operator delete(void *, const ::std:: nothrow_t&)
	throw ();	

void __cdecl operator delete[](void *, const ::std:: nothrow_t&)
	throw ();	
 


 
using ::std:: new_handler;
 

 
 #pragma warning(pop)
 #pragma pack(pop)











































dtQueryFilter::dtQueryFilter() :
	m_includeFlags(0xffff),
	m_excludeFlags(0)
{
	for (int i = 0; i < DT_MAX_AREAS; ++i)
		m_areaCost[i] = 1.0f;
}

















inline bool dtQueryFilter::passFilter(const dtPolyRef ,
									  const dtMeshTile* ,
									  const dtPoly* poly) const
{
	return (poly->flags & m_includeFlags) != 0 && (poly->flags & m_excludeFlags) == 0;
}

inline float dtQueryFilter::getCost(const float* pa, const float* pb,
									const dtPolyRef , const dtMeshTile* , const dtPoly* ,
									const dtPolyRef , const dtMeshTile* , const dtPoly* curPoly,
									const dtPolyRef , const dtMeshTile* , const dtPoly* ) const
{
	return dtVdist(pa, pb) * m_areaCost[curPoly->getArea()];
}

	
static const float H_SCALE = 0.999f; 


dtNavMeshQuery* dtAllocNavMeshQuery()
{
	void* mem = dtAlloc(sizeof(dtNavMeshQuery), DT_ALLOC_PERM);
	if (!mem) return 0;
	return new(mem) dtNavMeshQuery;
}

void dtFreeNavMeshQuery(dtNavMeshQuery* navmesh)
{
	if (!navmesh) return;
	navmesh->~dtNavMeshQuery();
	dtFree(navmesh);
}



















dtNavMeshQuery::dtNavMeshQuery() :
	m_nav(0),
	m_tinyNodePool(0),
	m_nodePool(0),
	m_openList(0)
{
	memset(&m_query, 0, sizeof(dtQueryData));
}

dtNavMeshQuery::~dtNavMeshQuery()
{
	if (m_tinyNodePool)
		m_tinyNodePool->~dtNodePool();
	if (m_nodePool)
		m_nodePool->~dtNodePool();
	if (m_openList)
		m_openList->~dtNodeQueue();
	dtFree(m_tinyNodePool);
	dtFree(m_nodePool);
	dtFree(m_openList);
}







dtStatus dtNavMeshQuery::init(const dtNavMesh* nav, const int maxNodes)
{
	m_nav = nav;
	
	if (!m_nodePool || m_nodePool->getMaxNodes() < maxNodes)
	{
		if (m_nodePool)
		{
			m_nodePool->~dtNodePool();
			dtFree(m_nodePool);
			m_nodePool = 0;
		}
		m_nodePool = new (dtAlloc(sizeof(dtNodePool), DT_ALLOC_PERM)) dtNodePool(maxNodes, dtNextPow2(maxNodes/4));
		if (!m_nodePool)
			return DT_FAILURE | DT_OUT_OF_MEMORY;
	}
	else
	{
		m_nodePool->clear();
	}
	
	if (!m_tinyNodePool)
	{
		m_tinyNodePool = new (dtAlloc(sizeof(dtNodePool), DT_ALLOC_PERM)) dtNodePool(64, 32);
		if (!m_tinyNodePool)
			return DT_FAILURE | DT_OUT_OF_MEMORY;
	}
	else
	{
		m_tinyNodePool->clear();
	}
	
	
	if (!m_openList || m_openList->getCapacity() < maxNodes)
	{
		if (m_openList)
		{
			m_openList->~dtNodeQueue();
			dtFree(m_openList);
			m_openList = 0;
		}
		m_openList = new (dtAlloc(sizeof(dtNodeQueue), DT_ALLOC_PERM)) dtNodeQueue(maxNodes);
		if (!m_openList)
			return DT_FAILURE | DT_OUT_OF_MEMORY;
	}
	else
	{
		m_openList->clear();
	}
	
	return DT_SUCCESS;
}

dtStatus dtNavMeshQuery::findRandomPoint(const dtQueryFilter* filter, float (*frand)(),
										 dtPolyRef* randomRef, float* randomPt) const
{
	(void)( (!!(m_nav)) || (_wassert(L"m_nav", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detournavmeshquery.cpp", 222), 0) );
	
	
	const dtMeshTile* tile = 0;
	float tsum = 0.0f;
	for (int i = 0; i < m_nav->getMaxTiles(); i++)
	{
		const dtMeshTile* t = m_nav->getTile(i);
		if (!t || !t->header) continue;
		
		
		const float area = 1.0f; 
		tsum += area;
		const float u = frand();
		if (u*tsum <= area)
			tile = t;
	}
	if (!tile)
		return DT_FAILURE;

	
	const dtPoly* poly = 0;
	dtPolyRef polyRef = 0;
	const dtPolyRef base = m_nav->getPolyRefBase(tile);

	float areaSum = 0.0f;
	for (int i = 0; i < tile->header->polyCount; ++i)
	{
		const dtPoly* p = &tile->polys[i];
		
		if (p->getType() != DT_POLYTYPE_GROUND)
			continue;
		
		const dtPolyRef ref = base | (dtPolyRef)i;
		if (!filter->passFilter(ref, tile, p))
			continue;

		
		float polyArea = 0.0f;
		for (int j = 2; j < p->vertCount; ++j)
		{
			const float* va = &tile->verts[p->verts[0]*3];
			const float* vb = &tile->verts[p->verts[j-1]*3];
			const float* vc = &tile->verts[p->verts[j]*3];
			polyArea += dtTriArea2D(va,vb,vc);
		}

		
		areaSum += polyArea;
		const float u = frand();
		if (u*areaSum <= polyArea)
		{
			poly = p;
			polyRef = ref;
		}
	}
	
	if (!poly)
		return DT_FAILURE;

	
	const float* v = &tile->verts[poly->verts[0]*3];
	float verts[3*DT_VERTS_PER_POLYGON];
	float areas[DT_VERTS_PER_POLYGON];
	dtVcopy(&verts[0*3],v);
	for (int j = 1; j < poly->vertCount; ++j)
	{
		v = &tile->verts[poly->verts[j]*3];
		dtVcopy(&verts[j*3],v);
	}
	
	const float s = frand();
	const float t = frand();
	
	float pt[3];
	dtRandomPointInConvexPoly(verts, poly->vertCount, areas, s, t, pt);
	
	float h = 0.0f;
	dtStatus status = getPolyHeight(polyRef, pt, &h);
	if (dtStatusFailed(status))
		return status;
	pt[1] = h;
	
	dtVcopy(randomPt, pt);
	*randomRef = polyRef;

	return DT_SUCCESS;
}

dtStatus dtNavMeshQuery::findRandomPointAroundCircle(dtPolyRef startRef, const float* centerPos, const float radius,
													 const dtQueryFilter* filter, float (*frand)(),
													 dtPolyRef* randomRef, float* randomPt) const
{
	(void)( (!!(m_nav)) || (_wassert(L"m_nav", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detournavmeshquery.cpp", 315), 0) );
	(void)( (!!(m_nodePool)) || (_wassert(L"m_nodePool", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detournavmeshquery.cpp", 316), 0) );
	(void)( (!!(m_openList)) || (_wassert(L"m_openList", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detournavmeshquery.cpp", 317), 0) );
	
	
	if (!startRef || !m_nav->isValidPolyRef(startRef))
		return DT_FAILURE | DT_INVALID_PARAM;
	
	const dtMeshTile* startTile = 0;
	const dtPoly* startPoly = 0;
	m_nav->getTileAndPolyByRefUnsafe(startRef, &startTile, &startPoly);
	if (!filter->passFilter(startRef, startTile, startPoly))
		return DT_FAILURE | DT_INVALID_PARAM;
	
	m_nodePool->clear();
	m_openList->clear();
	
	dtNode* startNode = m_nodePool->getNode(startRef);
	dtVcopy(startNode->pos, centerPos);
	startNode->pidx = 0;
	startNode->cost = 0;
	startNode->total = 0;
	startNode->id = startRef;
	startNode->flags = DT_NODE_OPEN;
	m_openList->push(startNode);
	
	dtStatus status = DT_SUCCESS;
	
	const float radiusSqr = dtSqr(radius);
	float areaSum = 0.0f;

	const dtMeshTile* randomTile = 0;
	const dtPoly* randomPoly = 0;
	dtPolyRef randomPolyRef = 0;

	while (!m_openList->empty())
	{
		dtNode* bestNode = m_openList->pop();
		bestNode->flags &= ~DT_NODE_OPEN;
		bestNode->flags |= DT_NODE_CLOSED;
		
		
		
		const dtPolyRef bestRef = bestNode->id;
		const dtMeshTile* bestTile = 0;
		const dtPoly* bestPoly = 0;
		m_nav->getTileAndPolyByRefUnsafe(bestRef, &bestTile, &bestPoly);

		
		if (bestPoly->getType() == DT_POLYTYPE_GROUND)
		{
			
			float polyArea = 0.0f;
			for (int j = 2; j < bestPoly->vertCount; ++j)
			{
				const float* va = &bestTile->verts[bestPoly->verts[0]*3];
				const float* vb = &bestTile->verts[bestPoly->verts[j-1]*3];
				const float* vc = &bestTile->verts[bestPoly->verts[j]*3];
				polyArea += dtTriArea2D(va,vb,vc);
			}
			
			areaSum += polyArea;
			const float u = frand();
			if (u*areaSum <= polyArea)
			{
				randomTile = bestTile;
				randomPoly = bestPoly;
				randomPolyRef = bestRef;
			}
		}
		
		
		
		dtPolyRef parentRef = 0;
		const dtMeshTile* parentTile = 0;
		const dtPoly* parentPoly = 0;
		if (bestNode->pidx)
			parentRef = m_nodePool->getNodeAtIdx(bestNode->pidx)->id;
		if (parentRef)
			m_nav->getTileAndPolyByRefUnsafe(parentRef, &parentTile, &parentPoly);
		
		for (unsigned int i = bestPoly->firstLink; i != DT_NULL_LINK; i = bestTile->links[i].next)
		{
			const dtLink* link = &bestTile->links[i];
			dtPolyRef neighbourRef = link->ref;
			
			if (!neighbourRef || neighbourRef == parentRef)
				continue;
			
			
			const dtMeshTile* neighbourTile = 0;
			const dtPoly* neighbourPoly = 0;
			m_nav->getTileAndPolyByRefUnsafe(neighbourRef, &neighbourTile, &neighbourPoly);
			
			
			if (!filter->passFilter(neighbourRef, neighbourTile, neighbourPoly))
				continue;
			
			
			float va[3], vb[3];
			if (!getPortalPoints(bestRef, bestPoly, bestTile, neighbourRef, neighbourPoly, neighbourTile, va, vb))
				continue;
			
			
			float tseg;
			float distSqr = dtDistancePtSegSqr2D(centerPos, va, vb, tseg);
			if (distSqr > radiusSqr)
				continue;
			
			dtNode* neighbourNode = m_nodePool->getNode(neighbourRef);
			if (!neighbourNode)
			{
				status |= DT_OUT_OF_NODES;
				continue;
			}
			
			if (neighbourNode->flags & DT_NODE_CLOSED)
				continue;
			
			
			if (neighbourNode->flags == 0)
				dtVlerp(neighbourNode->pos, va, vb, 0.5f);
			
			const float total = bestNode->total + dtVdist(bestNode->pos, neighbourNode->pos);
			
			
			if ((neighbourNode->flags & DT_NODE_OPEN) && total >= neighbourNode->total)
				continue;
			
			neighbourNode->id = neighbourRef;
			neighbourNode->flags = (neighbourNode->flags & ~DT_NODE_CLOSED);
			neighbourNode->pidx = m_nodePool->getNodeIdx(bestNode);
			neighbourNode->total = total;
			
			if (neighbourNode->flags & DT_NODE_OPEN)
			{
				m_openList->modify(neighbourNode);
			}
			else
			{
				neighbourNode->flags = DT_NODE_OPEN;
				m_openList->push(neighbourNode);
			}
		}
	}
	
	if (!randomPoly)
		return DT_FAILURE;
	
	
	const float* v = &randomTile->verts[randomPoly->verts[0]*3];
	float verts[3*DT_VERTS_PER_POLYGON];
	float areas[DT_VERTS_PER_POLYGON];
	dtVcopy(&verts[0*3],v);
	for (int j = 1; j < randomPoly->vertCount; ++j)
	{
		v = &randomTile->verts[randomPoly->verts[j]*3];
		dtVcopy(&verts[j*3],v);
	}
	
	const float s = frand();
	const float t = frand();
	
	float pt[3];
	dtRandomPointInConvexPoly(verts, randomPoly->vertCount, areas, s, t, pt);
	
	float h = 0.0f;
	dtStatus stat = getPolyHeight(randomPolyRef, pt, &h);
	if (dtStatusFailed(status))
		return stat;
	pt[1] = h;
	
	dtVcopy(randomPt, pt);
	*randomRef = randomPolyRef;
	
	return DT_SUCCESS;
}












dtStatus dtNavMeshQuery::closestPointOnPoly(dtPolyRef ref, const float* pos, float* closest) const
{
	(void)( (!!(m_nav)) || (_wassert(L"m_nav", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detournavmeshquery.cpp", 506), 0) );
	const dtMeshTile* tile = 0;
	const dtPoly* poly = 0;
	if (dtStatusFailed(m_nav->getTileAndPolyByRef(ref, &tile, &poly)))
		return DT_FAILURE | DT_INVALID_PARAM;
	if (!tile)
		return DT_FAILURE | DT_INVALID_PARAM;
	
	closestPointOnPolyInTile(tile, poly, pos, closest);
	
	return DT_SUCCESS;
}

void dtNavMeshQuery::closestPointOnPolyInTile(const dtMeshTile* tile, const dtPoly* poly,
											  const float* pos, float* closest) const
{
	
	if (poly->getType() == DT_POLYTYPE_OFFMESH_CONNECTION)
	{
		const float* v0 = &tile->verts[poly->verts[0]*3];
		const float* v1 = &tile->verts[poly->verts[1]*3];
		const float d0 = dtVdist(pos, v0);
		const float d1 = dtVdist(pos, v1);
		const float u = d0 / (d0+d1);
		dtVlerp(closest, v0, v1, u);
		return;
	}

	const unsigned int ip = (unsigned int)(poly - tile->polys);
	const dtPolyDetail* pd = &tile->detailMeshes[ip];

	
	float verts[DT_VERTS_PER_POLYGON*3];	
	float edged[DT_VERTS_PER_POLYGON];
	float edget[DT_VERTS_PER_POLYGON];
	const int nv = poly->vertCount;
	for (int i = 0; i < nv; ++i)
		dtVcopy(&verts[i*3], &tile->verts[poly->verts[i]*3]);
	
	dtVcopy(closest, pos);
	if (!dtDistancePtPolyEdgesSqr(pos, verts, nv, edged, edget))
	{
		
		float dmin = 3.402823466e+38F;
		int imin = -1;
		for (int i = 0; i < nv; ++i)
		{
			if (edged[i] < dmin)
			{
				dmin = edged[i];
				imin = i;
			}
		}
		const float* va = &verts[imin*3];
		const float* vb = &verts[((imin+1)%nv)*3];
		dtVlerp(closest, va, vb, edget[imin]);
	}

	
	for (int j = 0; j < pd->triCount; ++j)
	{
		const unsigned char* t = &tile->detailTris[(pd->triBase+j)*4];
		const float* v[3];
		for (int k = 0; k < 3; ++k)
		{
			if (t[k] < poly->vertCount)
				v[k] = &tile->verts[poly->verts[t[k]]*3];
			else
				v[k] = &tile->detailVerts[(pd->vertBase+(t[k]-poly->vertCount))*3];
		}
		float h;
		if (dtClosestHeightPointTriangle(pos, v[0], v[1], v[2], h))
		{
			closest[1] = h;
			break;
		}
	}
























}












dtStatus dtNavMeshQuery::closestPointOnPolyBoundary(dtPolyRef ref, const float* pos, float* closest) const
{
	(void)( (!!(m_nav)) || (_wassert(L"m_nav", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detournavmeshquery.cpp", 622), 0) );
	
	const dtMeshTile* tile = 0;
	const dtPoly* poly = 0;
	if (dtStatusFailed(m_nav->getTileAndPolyByRef(ref, &tile, &poly)))
		return DT_FAILURE | DT_INVALID_PARAM;
	
	
	float verts[DT_VERTS_PER_POLYGON*3];	
	float edged[DT_VERTS_PER_POLYGON];
	float edget[DT_VERTS_PER_POLYGON];
	int nv = 0;
	for (int i = 0; i < (int)poly->vertCount; ++i)
	{
		dtVcopy(&verts[nv*3], &tile->verts[poly->verts[i]*3]);
		nv++;
	}		
	
	bool inside = dtDistancePtPolyEdgesSqr(pos, verts, nv, edged, edget);
	if (inside)
	{
		
		dtVcopy(closest, pos);
	}
	else
	{
		
		float dmin = 3.402823466e+38F;
		int imin = -1;
		for (int i = 0; i < nv; ++i)
		{
			if (edged[i] < dmin)
			{
				dmin = edged[i];
				imin = i;
			}
		}
		const float* va = &verts[imin*3];
		const float* vb = &verts[((imin+1)%nv)*3];
		dtVlerp(closest, va, vb, edget[imin]);
	}
	
	return DT_SUCCESS;
}






dtStatus dtNavMeshQuery::getPolyHeight(dtPolyRef ref, const float* pos, float* height) const
{
	(void)( (!!(m_nav)) || (_wassert(L"m_nav", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detournavmeshquery.cpp", 674), 0) );

	const dtMeshTile* tile = 0;
	const dtPoly* poly = 0;
	if (dtStatusFailed(m_nav->getTileAndPolyByRef(ref, &tile, &poly)))
		return DT_FAILURE | DT_INVALID_PARAM;
	
	if (poly->getType() == DT_POLYTYPE_OFFMESH_CONNECTION)
	{
		const float* v0 = &tile->verts[poly->verts[0]*3];
		const float* v1 = &tile->verts[poly->verts[1]*3];
		const float d0 = dtVdist(pos, v0);
		const float d1 = dtVdist(pos, v1);
		const float u = d0 / (d0+d1);
		if (height)
			*height = v0[1] + (v1[1] - v0[1]) * u;
		return DT_SUCCESS;
	}
	else
	{
		const unsigned int ip = (unsigned int)(poly - tile->polys);
		const dtPolyDetail* pd = &tile->detailMeshes[ip];
		for (int j = 0; j < pd->triCount; ++j)
		{
			const unsigned char* t = &tile->detailTris[(pd->triBase+j)*4];
			const float* v[3];
			for (int k = 0; k < 3; ++k)
			{
				if (t[k] < poly->vertCount)
					v[k] = &tile->verts[poly->verts[t[k]]*3];
				else
					v[k] = &tile->detailVerts[(pd->vertBase+(t[k]-poly->vertCount))*3];
			}
			float h;
			if (dtClosestHeightPointTriangle(pos, v[0], v[1], v[2], h))
			{
				if (height)
					*height = h;
				return DT_SUCCESS;
			}
		}
	}
	
	return DT_FAILURE | DT_INVALID_PARAM;
}










dtStatus dtNavMeshQuery::findNearestPoly(const float* center, const float* extents,
										 const dtQueryFilter* filter,
										 dtPolyRef* nearestRef, float* nearestPt) const
{
	(void)( (!!(m_nav)) || (_wassert(L"m_nav", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detournavmeshquery.cpp", 733), 0) );

	*nearestRef = 0;
	
	
	dtPolyRef polys[128];
	int polyCount = 0;
	if (dtStatusFailed(queryPolygons(center, extents, filter, polys, &polyCount, 128)))
		return DT_FAILURE | DT_INVALID_PARAM;
	
	
	dtPolyRef nearest = 0;
	float nearestDistanceSqr = 3.402823466e+38F;
	for (int i = 0; i < polyCount; ++i)
	{
		dtPolyRef ref = polys[i];
		float closestPtPoly[3];
		closestPointOnPoly(ref, center, closestPtPoly);
		float d = dtVdistSqr(center, closestPtPoly);
		if (d < nearestDistanceSqr)
		{
			if (nearestPt)
				dtVcopy(nearestPt, closestPtPoly);
			nearestDistanceSqr = d;
			nearest = ref;
		}
	}
	
	if (nearestRef)
		*nearestRef = nearest;
	
	return DT_SUCCESS;
}

dtPolyRef dtNavMeshQuery::findNearestPolyInTile(const dtMeshTile* tile, const float* center, const float* extents,
												const dtQueryFilter* filter, float* nearestPt) const
{
	(void)( (!!(m_nav)) || (_wassert(L"m_nav", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detournavmeshquery.cpp", 770), 0) );
	
	float bmin[3], bmax[3];
	dtVsub(bmin, center, extents);
	dtVadd(bmax, center, extents);
	
	
	dtPolyRef polys[128];
	int polyCount = queryPolygonsInTile(tile, bmin, bmax, filter, polys, 128);
	
	
	dtPolyRef nearest = 0;
	float nearestDistanceSqr = 3.402823466e+38F;
	for (int i = 0; i < polyCount; ++i)
	{
		dtPolyRef ref = polys[i];
		const dtPoly* poly = &tile->polys[m_nav->decodePolyIdPoly(ref)];
		float closestPtPoly[3];
		closestPointOnPolyInTile(tile, poly, center, closestPtPoly);
			
		float d = dtVdistSqr(center, closestPtPoly);
		if (d < nearestDistanceSqr)
		{
			if (nearestPt)
				dtVcopy(nearestPt, closestPtPoly);
			nearestDistanceSqr = d;
			nearest = ref;
		}
	}
	
	return nearest;
}

int dtNavMeshQuery::queryPolygonsInTile(const dtMeshTile* tile, const float* qmin, const float* qmax,
										const dtQueryFilter* filter,
										dtPolyRef* polys, const int maxPolys) const
{
	(void)( (!!(m_nav)) || (_wassert(L"m_nav", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detournavmeshquery.cpp", 807), 0) );

	if (tile->bvTree)
	{
		const dtBVNode* node = &tile->bvTree[0];
		const dtBVNode* end = &tile->bvTree[tile->header->bvNodeCount];
		const float* tbmin = tile->header->bmin;
		const float* tbmax = tile->header->bmax;
		const float qfac = tile->header->bvQuantFactor;
		
		
		unsigned short bmin[3], bmax[3];
		
		float minx = dtClamp(qmin[0], tbmin[0], tbmax[0]) - tbmin[0];
		float miny = dtClamp(qmin[1], tbmin[1], tbmax[1]) - tbmin[1];
		float minz = dtClamp(qmin[2], tbmin[2], tbmax[2]) - tbmin[2];
		float maxx = dtClamp(qmax[0], tbmin[0], tbmax[0]) - tbmin[0];
		float maxy = dtClamp(qmax[1], tbmin[1], tbmax[1]) - tbmin[1];
		float maxz = dtClamp(qmax[2], tbmin[2], tbmax[2]) - tbmin[2];
		
		bmin[0] = (unsigned short)(qfac * minx) & 0xfffe;
		bmin[1] = (unsigned short)(qfac * miny) & 0xfffe;
		bmin[2] = (unsigned short)(qfac * minz) & 0xfffe;
		bmax[0] = (unsigned short)(qfac * maxx + 1) | 1;
		bmax[1] = (unsigned short)(qfac * maxy + 1) | 1;
		bmax[2] = (unsigned short)(qfac * maxz + 1) | 1;
		
		
		const dtPolyRef base = m_nav->getPolyRefBase(tile);
		int n = 0;
		while (node < end)
		{
			const bool overlap = dtOverlapQuantBounds(bmin, bmax, node->bmin, node->bmax);
			const bool isLeafNode = node->i >= 0;
			
			if (isLeafNode && overlap)
			{
				dtPolyRef ref = base | (dtPolyRef)node->i;
				if (filter->passFilter(ref, tile, &tile->polys[node->i]))
				{
					if (n < maxPolys)
						polys[n++] = ref;
				}
			}
			
			if (overlap || isLeafNode)
				node++;
			else
			{
				const int escapeIndex = -node->i;
				node += escapeIndex;
			}
		}
		
		return n;
	}
	else
	{
		float bmin[3], bmax[3];
		int n = 0;
		const dtPolyRef base = m_nav->getPolyRefBase(tile);
		for (int i = 0; i < tile->header->polyCount; ++i)
		{
			const dtPoly* p = &tile->polys[i];
			
			if (p->getType() == DT_POLYTYPE_OFFMESH_CONNECTION)
				continue;
			
			const dtPolyRef ref = base | (dtPolyRef)i;
			if (!filter->passFilter(ref, tile, p))
				continue;
			
			const float* v = &tile->verts[p->verts[0]*3];
			dtVcopy(bmin, v);
			dtVcopy(bmax, v);
			for (int j = 1; j < p->vertCount; ++j)
			{
				v = &tile->verts[p->verts[j]*3];
				dtVmin(bmin, v);
				dtVmax(bmax, v);
			}
			if (dtOverlapBounds(qmin,qmax, bmin,bmax))
			{
				if (n < maxPolys)
					polys[n++] = ref;
			}
		}
		return n;
	}
}










dtStatus dtNavMeshQuery::queryPolygons(const float* center, const float* extents,
									   const dtQueryFilter* filter,
									   dtPolyRef* polys, int* polyCount, const int maxPolys) const
{
	(void)( (!!(m_nav)) || (_wassert(L"m_nav", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detournavmeshquery.cpp", 911), 0) );
	
	float bmin[3], bmax[3];
	dtVsub(bmin, center, extents);
	dtVadd(bmax, center, extents);
	
	
	int minx, miny, maxx, maxy;
	m_nav->calcTileLoc(bmin, &minx, &miny);
	m_nav->calcTileLoc(bmax, &maxx, &maxy);

	static const int MAX_NEIS = 32;
	const dtMeshTile* neis[MAX_NEIS];
	
	int n = 0;
	for (int y = miny; y <= maxy; ++y)
	{
		for (int x = minx; x <= maxx; ++x)
		{
			const int nneis = m_nav->getTilesAt(x,y,neis,MAX_NEIS);
			for (int j = 0; j < nneis; ++j)
			{
				n += queryPolygonsInTile(neis[j], bmin, bmax, filter, polys+n, maxPolys-n);
				if (n >= maxPolys)
				{
					*polyCount = n;
					return DT_SUCCESS | DT_BUFFER_TOO_SMALL;
				}
			}
		}
	}
	*polyCount = n;
	
	return DT_SUCCESS;
}












dtStatus dtNavMeshQuery::findPath(dtPolyRef startRef, dtPolyRef endRef,
								  const float* startPos, const float* endPos,
								  const dtQueryFilter* filter,
								  dtPolyRef* path, int* pathCount, const int maxPath) const
{
	(void)( (!!(m_nav)) || (_wassert(L"m_nav", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detournavmeshquery.cpp", 963), 0) );
	(void)( (!!(m_nodePool)) || (_wassert(L"m_nodePool", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detournavmeshquery.cpp", 964), 0) );
	(void)( (!!(m_openList)) || (_wassert(L"m_openList", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detournavmeshquery.cpp", 965), 0) );
	
	*pathCount = 0;
	
	if (!startRef || !endRef)
		return DT_FAILURE | DT_INVALID_PARAM;
	
	if (!maxPath)
		return DT_FAILURE | DT_INVALID_PARAM;
	
	
	if (!m_nav->isValidPolyRef(startRef) || !m_nav->isValidPolyRef(endRef))
		return DT_FAILURE | DT_INVALID_PARAM;
	
	if (startRef == endRef)
	{
		path[0] = startRef;
		*pathCount = 1;
		return DT_SUCCESS;
	}
	
	m_nodePool->clear();
	m_openList->clear();
	
	dtNode* startNode = m_nodePool->getNode(startRef);
	dtVcopy(startNode->pos, startPos);
	startNode->pidx = 0;
	startNode->cost = 0;
	startNode->total = dtVdist(startPos, endPos) * H_SCALE;
	startNode->id = startRef;
	startNode->flags = DT_NODE_OPEN;
	m_openList->push(startNode);
	
	dtNode* lastBestNode = startNode;
	float lastBestNodeCost = startNode->total;
	
	dtStatus status = DT_SUCCESS;
	
	while (!m_openList->empty())
	{
		
		dtNode* bestNode = m_openList->pop();
		bestNode->flags &= ~DT_NODE_OPEN;
		bestNode->flags |= DT_NODE_CLOSED;
		
		
		if (bestNode->id == endRef)
		{
			lastBestNode = bestNode;
			break;
		}
		
		
		
		const dtPolyRef bestRef = bestNode->id;
		const dtMeshTile* bestTile = 0;
		const dtPoly* bestPoly = 0;
		m_nav->getTileAndPolyByRefUnsafe(bestRef, &bestTile, &bestPoly);
		
		
		dtPolyRef parentRef = 0;
		const dtMeshTile* parentTile = 0;
		const dtPoly* parentPoly = 0;
		if (bestNode->pidx)
			parentRef = m_nodePool->getNodeAtIdx(bestNode->pidx)->id;
		if (parentRef)
			m_nav->getTileAndPolyByRefUnsafe(parentRef, &parentTile, &parentPoly);
		
		for (unsigned int i = bestPoly->firstLink; i != DT_NULL_LINK; i = bestTile->links[i].next)
		{
			dtPolyRef neighbourRef = bestTile->links[i].ref;
			
			
			if (!neighbourRef || neighbourRef == parentRef)
				continue;
			
			
			
			const dtMeshTile* neighbourTile = 0;
			const dtPoly* neighbourPoly = 0;
			m_nav->getTileAndPolyByRefUnsafe(neighbourRef, &neighbourTile, &neighbourPoly);			
			
			if (!filter->passFilter(neighbourRef, neighbourTile, neighbourPoly))
				continue;

			dtNode* neighbourNode = m_nodePool->getNode(neighbourRef);
			if (!neighbourNode)
			{
				status |= DT_OUT_OF_NODES;
				continue;
			}
			
			
			if (neighbourNode->flags == 0)
			{
				getEdgeMidPoint(bestRef, bestPoly, bestTile,
								neighbourRef, neighbourPoly, neighbourTile,
								neighbourNode->pos);
			}

			
			float cost = 0;
			float heuristic = 0;
			
			
			if (neighbourRef == endRef)
			{
				
				const float curCost = filter->getCost(bestNode->pos, neighbourNode->pos,
													  parentRef, parentTile, parentPoly,
													  bestRef, bestTile, bestPoly,
													  neighbourRef, neighbourTile, neighbourPoly);
				const float endCost = filter->getCost(neighbourNode->pos, endPos,
													  bestRef, bestTile, bestPoly,
													  neighbourRef, neighbourTile, neighbourPoly,
													  0, 0, 0);
				
				cost = bestNode->cost + curCost + endCost;
				heuristic = 0;
			}
			else
			{
				
				const float curCost = filter->getCost(bestNode->pos, neighbourNode->pos,
													  parentRef, parentTile, parentPoly,
													  bestRef, bestTile, bestPoly,
													  neighbourRef, neighbourTile, neighbourPoly);
				cost = bestNode->cost + curCost;
				heuristic = dtVdist(neighbourNode->pos, endPos)*H_SCALE;
			}

			const float total = cost + heuristic;
			
			
			if ((neighbourNode->flags & DT_NODE_OPEN) && total >= neighbourNode->total)
				continue;
			
			if ((neighbourNode->flags & DT_NODE_CLOSED) && total >= neighbourNode->total)
				continue;
			
			
			neighbourNode->pidx = m_nodePool->getNodeIdx(bestNode);
			neighbourNode->id = neighbourRef;
			neighbourNode->flags = (neighbourNode->flags & ~DT_NODE_CLOSED);
			neighbourNode->cost = cost;
			neighbourNode->total = total;
			
			if (neighbourNode->flags & DT_NODE_OPEN)
			{
				
				m_openList->modify(neighbourNode);
			}
			else
			{
				
				neighbourNode->flags |= DT_NODE_OPEN;
				m_openList->push(neighbourNode);
			}
			
			
			if (heuristic < lastBestNodeCost)
			{
				lastBestNodeCost = heuristic;
				lastBestNode = neighbourNode;
			}
		}
	}
	
	if (lastBestNode->id != endRef)
		status |= DT_PARTIAL_RESULT;
	
	
	dtNode* prev = 0;
	dtNode* node = lastBestNode;
	do
	{
		dtNode* next = m_nodePool->getNodeAtIdx(node->pidx);
		node->pidx = m_nodePool->getNodeIdx(prev);
		prev = node;
		node = next;
	}
	while (node);
	
	
	node = prev;
	int n = 0;
	do
	{
		path[n++] = node->id;
		if (n >= maxPath)
		{
			status |= DT_BUFFER_TOO_SMALL;
			break;
		}
		node = m_nodePool->getNodeAtIdx(node->pidx);
	}
	while (node);
	
	*pathCount = n;
	
	return status;
}









dtStatus dtNavMeshQuery::initSlicedFindPath(dtPolyRef startRef, dtPolyRef endRef,
											const float* startPos, const float* endPos,
											const dtQueryFilter* filter)
{
	(void)( (!!(m_nav)) || (_wassert(L"m_nav", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detournavmeshquery.cpp", 1180), 0) );
	(void)( (!!(m_nodePool)) || (_wassert(L"m_nodePool", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detournavmeshquery.cpp", 1181), 0) );
	(void)( (!!(m_openList)) || (_wassert(L"m_openList", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detournavmeshquery.cpp", 1182), 0) );

	
	memset(&m_query, 0, sizeof(dtQueryData));
	m_query.status = DT_FAILURE;
	m_query.startRef = startRef;
	m_query.endRef = endRef;
	dtVcopy(m_query.startPos, startPos);
	dtVcopy(m_query.endPos, endPos);
	m_query.filter = filter;
	
	if (!startRef || !endRef)
		return DT_FAILURE | DT_INVALID_PARAM;
	
	
	if (!m_nav->isValidPolyRef(startRef) || !m_nav->isValidPolyRef(endRef))
		return DT_FAILURE | DT_INVALID_PARAM;

	if (startRef == endRef)
	{
		m_query.status = DT_SUCCESS;
		return DT_SUCCESS;
	}
	
	m_nodePool->clear();
	m_openList->clear();
	
	dtNode* startNode = m_nodePool->getNode(startRef);
	dtVcopy(startNode->pos, startPos);
	startNode->pidx = 0;
	startNode->cost = 0;
	startNode->total = dtVdist(startPos, endPos) * H_SCALE;
	startNode->id = startRef;
	startNode->flags = DT_NODE_OPEN;
	m_openList->push(startNode);
	
	m_query.status = DT_IN_PROGRESS;
	m_query.lastBestNode = startNode;
	m_query.lastBestNodeCost = startNode->total;
	
	return m_query.status;
}
	
dtStatus dtNavMeshQuery::updateSlicedFindPath(const int maxIter, int* doneIters)
{
	if (!dtStatusInProgress(m_query.status))
		return m_query.status;

	
	if (!m_nav->isValidPolyRef(m_query.startRef) || !m_nav->isValidPolyRef(m_query.endRef))
	{
		m_query.status = DT_FAILURE;
		return DT_FAILURE;
	}
		
	int iter = 0;
	while (iter < maxIter && !m_openList->empty())
	{
		iter++;
		
		
		dtNode* bestNode = m_openList->pop();
		bestNode->flags &= ~DT_NODE_OPEN;
		bestNode->flags |= DT_NODE_CLOSED;
		
		
		if (bestNode->id == m_query.endRef)
		{
			m_query.lastBestNode = bestNode;
			const dtStatus details = m_query.status & DT_STATUS_DETAIL_MASK;
			m_query.status = DT_SUCCESS | details;
			if (doneIters)
				*doneIters = iter;
			return m_query.status;
		}
		
		
		
		const dtPolyRef bestRef = bestNode->id;
		const dtMeshTile* bestTile = 0;
		const dtPoly* bestPoly = 0;
		if (dtStatusFailed(m_nav->getTileAndPolyByRef(bestRef, &bestTile, &bestPoly)))
		{
			
			m_query.status = DT_FAILURE;
			if (doneIters)
				*doneIters = iter;
			return m_query.status;
		}
		
		
		dtPolyRef parentRef = 0;
		const dtMeshTile* parentTile = 0;
		const dtPoly* parentPoly = 0;
		if (bestNode->pidx)
			parentRef = m_nodePool->getNodeAtIdx(bestNode->pidx)->id;
		if (parentRef)
		{
			if (dtStatusFailed(m_nav->getTileAndPolyByRef(parentRef, &parentTile, &parentPoly)))
			{
				
				m_query.status = DT_FAILURE;
				if (doneIters)
					*doneIters = iter;
				return m_query.status;
			}
		}
		
		for (unsigned int i = bestPoly->firstLink; i != DT_NULL_LINK; i = bestTile->links[i].next)
		{
			dtPolyRef neighbourRef = bestTile->links[i].ref;
			
			
			if (!neighbourRef || neighbourRef == parentRef)
				continue;
			
			
			
			const dtMeshTile* neighbourTile = 0;
			const dtPoly* neighbourPoly = 0;
			m_nav->getTileAndPolyByRefUnsafe(neighbourRef, &neighbourTile, &neighbourPoly);			
			
			if (!m_query.filter->passFilter(neighbourRef, neighbourTile, neighbourPoly))
				continue;
			
			dtNode* neighbourNode = m_nodePool->getNode(neighbourRef);
			if (!neighbourNode)
			{
				m_query.status |= DT_OUT_OF_NODES;
				continue;
			}
			
			
			if (neighbourNode->flags == 0)
			{
				getEdgeMidPoint(bestRef, bestPoly, bestTile,
								neighbourRef, neighbourPoly, neighbourTile,
								neighbourNode->pos);
			}
			
			
			float cost = 0;
			float heuristic = 0;
			
			
			if (neighbourRef == m_query.endRef)
			{
				
				const float curCost = m_query.filter->getCost(bestNode->pos, neighbourNode->pos,
															  parentRef, parentTile, parentPoly,
															  bestRef, bestTile, bestPoly,
															  neighbourRef, neighbourTile, neighbourPoly);
				const float endCost = m_query.filter->getCost(neighbourNode->pos, m_query.endPos,
															  bestRef, bestTile, bestPoly,
															  neighbourRef, neighbourTile, neighbourPoly,
															  0, 0, 0);
				
				cost = bestNode->cost + curCost + endCost;
				heuristic = 0;
			}
			else
			{
				
				const float curCost = m_query.filter->getCost(bestNode->pos, neighbourNode->pos,
															  parentRef, parentTile, parentPoly,
															  bestRef, bestTile, bestPoly,
															  neighbourRef, neighbourTile, neighbourPoly);
				cost = bestNode->cost + curCost;
				heuristic = dtVdist(neighbourNode->pos, m_query.endPos)*H_SCALE;
			}
			
			const float total = cost + heuristic;
			
			
			if ((neighbourNode->flags & DT_NODE_OPEN) && total >= neighbourNode->total)
				continue;
			
			if ((neighbourNode->flags & DT_NODE_CLOSED) && total >= neighbourNode->total)
				continue;
			
			
			neighbourNode->pidx = m_nodePool->getNodeIdx(bestNode);
			neighbourNode->id = neighbourRef;
			neighbourNode->flags = (neighbourNode->flags & ~DT_NODE_CLOSED);
			neighbourNode->cost = cost;
			neighbourNode->total = total;
			
			if (neighbourNode->flags & DT_NODE_OPEN)
			{
				
				m_openList->modify(neighbourNode);
			}
			else
			{
				
				neighbourNode->flags |= DT_NODE_OPEN;
				m_openList->push(neighbourNode);
			}
			
			
			if (heuristic < m_query.lastBestNodeCost)
			{
				m_query.lastBestNodeCost = heuristic;
				m_query.lastBestNode = neighbourNode;
			}
		}
	}
	
	
	if (m_openList->empty())
	{
		const dtStatus details = m_query.status & DT_STATUS_DETAIL_MASK;
		m_query.status = DT_SUCCESS | details;
	}

	if (doneIters)
		*doneIters = iter;

	return m_query.status;
}

dtStatus dtNavMeshQuery::finalizeSlicedFindPath(dtPolyRef* path, int* pathCount, const int maxPath)
{
	*pathCount = 0;
	
	if (dtStatusFailed(m_query.status))
	{
		
		memset(&m_query, 0, sizeof(dtQueryData));
		return DT_FAILURE;
	}

	int n = 0;

	if (m_query.startRef == m_query.endRef)
	{
		
		path[n++] = m_query.startRef;
	}
	else
	{
		
		(void)( (!!(m_query.lastBestNode)) || (_wassert(L"m_query.lastBestNode", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detournavmeshquery.cpp", 1424), 0) );
		
		if (m_query.lastBestNode->id != m_query.endRef)
			m_query.status |= DT_PARTIAL_RESULT;
		
		dtNode* prev = 0;
		dtNode* node = m_query.lastBestNode;
		do
		{
			dtNode* next = m_nodePool->getNodeAtIdx(node->pidx);
			node->pidx = m_nodePool->getNodeIdx(prev);
			prev = node;
			node = next;
		}
		while (node);
		
		
		node = prev;
		do
		{
			path[n++] = node->id;
			if (n >= maxPath)
			{
				m_query.status |= DT_BUFFER_TOO_SMALL;
				break;
			}
			node = m_nodePool->getNodeAtIdx(node->pidx);
		}
		while (node);
	}
	
	const dtStatus details = m_query.status & DT_STATUS_DETAIL_MASK;

	
	memset(&m_query, 0, sizeof(dtQueryData));
	
	*pathCount = n;
	
	return DT_SUCCESS | details;
}

dtStatus dtNavMeshQuery::finalizeSlicedFindPathPartial(const dtPolyRef* existing, const int existingSize,
													   dtPolyRef* path, int* pathCount, const int maxPath)
{
	*pathCount = 0;
	
	if (existingSize == 0)
	{
		return DT_FAILURE;
	}
	
	if (dtStatusFailed(m_query.status))
	{
		
		memset(&m_query, 0, sizeof(dtQueryData));
		return DT_FAILURE;
	}
	
	int n = 0;
	
	if (m_query.startRef == m_query.endRef)
	{
		
		path[n++] = m_query.startRef;
	}
	else
	{
		
		dtNode* prev = 0;
		dtNode* node = 0;
		for (int i = existingSize-1; i >= 0; --i)
		{
			node = m_nodePool->findNode(existing[i]);
			if (node)
				break;
		}
		
		if (!node)
		{
			m_query.status |= DT_PARTIAL_RESULT;
			(void)( (!!(m_query.lastBestNode)) || (_wassert(L"m_query.lastBestNode", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detournavmeshquery.cpp", 1504), 0) );
			node = m_query.lastBestNode;
		}
		
		
		do
		{
			dtNode* next = m_nodePool->getNodeAtIdx(node->pidx);
			node->pidx = m_nodePool->getNodeIdx(prev);
			prev = node;
			node = next;
		}
		while (node);
		
		
		node = prev;
		do
		{
			path[n++] = node->id;
			if (n >= maxPath)
			{
				m_query.status |= DT_BUFFER_TOO_SMALL;
				break;
			}
			node = m_nodePool->getNodeAtIdx(node->pidx);
		}
		while (node);
	}
	
	const dtStatus details = m_query.status & DT_STATUS_DETAIL_MASK;

	
	memset(&m_query, 0, sizeof(dtQueryData));
	
	*pathCount = n;
	
	return DT_SUCCESS | details;
}


dtStatus dtNavMeshQuery::appendVertex(const float* pos, const unsigned char flags, const dtPolyRef ref,
									  float* straightPath, unsigned char* straightPathFlags, dtPolyRef* straightPathRefs,
									  int* straightPathCount, const int maxStraightPath) const
{
	if ((*straightPathCount) > 0 && dtVequal(&straightPath[((*straightPathCount)-1)*3], pos))
	{
		
		if (straightPathFlags)
			straightPathFlags[(*straightPathCount)-1] = flags;
		if (straightPathRefs)
			straightPathRefs[(*straightPathCount)-1] = ref;
	}
	else
	{
		
		dtVcopy(&straightPath[(*straightPathCount)*3], pos);
		if (straightPathFlags)
			straightPathFlags[(*straightPathCount)] = flags;
		if (straightPathRefs)
			straightPathRefs[(*straightPathCount)] = ref;
		(*straightPathCount)++;
		
		if (flags == DT_STRAIGHTPATH_END || (*straightPathCount) >= maxStraightPath)
		{
			return DT_SUCCESS | (((*straightPathCount) >= maxStraightPath) ? DT_BUFFER_TOO_SMALL : 0);
		}
	}
	return DT_IN_PROGRESS;
}

dtStatus dtNavMeshQuery::appendPortals(const int startIdx, const int endIdx, const float* endPos, const dtPolyRef* path,
									  float* straightPath, unsigned char* straightPathFlags, dtPolyRef* straightPathRefs,
									  int* straightPathCount, const int maxStraightPath, const int options) const
{
	const float* startPos = &straightPath[(*straightPathCount-1)*3];
	
	dtStatus stat = 0;
	for (int i = startIdx; i < endIdx; i++)
	{
		
		const dtPolyRef from = path[i];
		const dtMeshTile* fromTile = 0;
		const dtPoly* fromPoly = 0;
		if (dtStatusFailed(m_nav->getTileAndPolyByRef(from, &fromTile, &fromPoly)))
			return DT_FAILURE | DT_INVALID_PARAM;
		
		const dtPolyRef to = path[i+1];
		const dtMeshTile* toTile = 0;
		const dtPoly* toPoly = 0;
		if (dtStatusFailed(m_nav->getTileAndPolyByRef(to, &toTile, &toPoly)))
			return DT_FAILURE | DT_INVALID_PARAM;
		
		float left[3], right[3];
		if (dtStatusFailed(getPortalPoints(from, fromPoly, fromTile, to, toPoly, toTile, left, right)))
			break;
	
		if (options & DT_STRAIGHTPATH_AREA_CROSSINGS)
		{
			
			if (fromPoly->getArea() == toPoly->getArea())
				continue;
		}
		
		
		float s,t;
		if (dtIntersectSegSeg2D(startPos, endPos, left, right, s, t))
		{
			float pt[3];
			dtVlerp(pt, left,right, t);

			stat = appendVertex(pt, 0, path[i+1],
								straightPath, straightPathFlags, straightPathRefs,
								straightPathCount, maxStraightPath);
			if (stat != DT_IN_PROGRESS)
				return stat;
		}
	}
	return DT_IN_PROGRESS;
}


















dtStatus dtNavMeshQuery::findStraightPath(const float* startPos, const float* endPos,
										  const dtPolyRef* path, const int pathSize,
										  float* straightPath, unsigned char* straightPathFlags, dtPolyRef* straightPathRefs,
										  int* straightPathCount, const int maxStraightPath, const int options) const
{
	(void)( (!!(m_nav)) || (_wassert(L"m_nav", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detournavmeshquery.cpp", 1646), 0) );
	
	*straightPathCount = 0;
	
	if (!maxStraightPath)
		return DT_FAILURE | DT_INVALID_PARAM;
	
	if (!path[0])
		return DT_FAILURE | DT_INVALID_PARAM;
	
	dtStatus stat = 0;
	
	
	float closestStartPos[3];
	if (dtStatusFailed(closestPointOnPolyBoundary(path[0], startPos, closestStartPos)))
		return DT_FAILURE | DT_INVALID_PARAM;

	float closestEndPos[3];
	if (dtStatusFailed(closestPointOnPolyBoundary(path[pathSize-1], endPos, closestEndPos)))
		return DT_FAILURE | DT_INVALID_PARAM;
	
	
	stat = appendVertex(closestStartPos, DT_STRAIGHTPATH_START, path[0],
						straightPath, straightPathFlags, straightPathRefs,
						straightPathCount, maxStraightPath);
	if (stat != DT_IN_PROGRESS)
		return stat;
	
	if (pathSize > 1)
	{
		float portalApex[3], portalLeft[3], portalRight[3];
		dtVcopy(portalApex, closestStartPos);
		dtVcopy(portalLeft, portalApex);
		dtVcopy(portalRight, portalApex);
		int apexIndex = 0;
		int leftIndex = 0;
		int rightIndex = 0;
		
		unsigned char leftPolyType = 0;
		unsigned char rightPolyType = 0;
		
		dtPolyRef leftPolyRef = path[0];
		dtPolyRef rightPolyRef = path[0];
		
		for (int i = 0; i < pathSize; ++i)
		{
			float left[3], right[3];
			unsigned char fromType, toType;
			
			if (i+1 < pathSize)
			{
				
				if (dtStatusFailed(getPortalPoints(path[i], path[i+1], left, right, fromType, toType)))
				{
					
					
					
					if (dtStatusFailed(closestPointOnPolyBoundary(path[i], endPos, closestEndPos)))
					{
						
						return DT_FAILURE | DT_INVALID_PARAM;
					}

					
					if (options & (DT_STRAIGHTPATH_AREA_CROSSINGS | DT_STRAIGHTPATH_ALL_CROSSINGS))
					{
						stat = appendPortals(apexIndex, i, closestEndPos, path,
											 straightPath, straightPathFlags, straightPathRefs,
											 straightPathCount, maxStraightPath, options);
					}

					stat = appendVertex(closestEndPos, 0, path[i],
										straightPath, straightPathFlags, straightPathRefs,
										straightPathCount, maxStraightPath);
					
					return DT_SUCCESS | DT_PARTIAL_RESULT | ((*straightPathCount >= maxStraightPath) ? DT_BUFFER_TOO_SMALL : 0);
				}
				
				
				if (i == 0)
				{
					float t;
					if (dtDistancePtSegSqr2D(portalApex, left, right, t) < dtSqr(0.001f))
						continue;
				}
			}
			else
			{
				
				dtVcopy(left, closestEndPos);
				dtVcopy(right, closestEndPos);
				
				fromType = toType = DT_POLYTYPE_GROUND;
			}
			
			
			if (dtTriArea2D(portalApex, portalRight, right) <= 0.0f)
			{
				if (dtVequal(portalApex, portalRight) || dtTriArea2D(portalApex, portalLeft, right) > 0.0f)
				{
					dtVcopy(portalRight, right);
					rightPolyRef = (i+1 < pathSize) ? path[i+1] : 0;
					rightPolyType = toType;
					rightIndex = i;
				}
				else
				{
					
					if (options & (DT_STRAIGHTPATH_AREA_CROSSINGS | DT_STRAIGHTPATH_ALL_CROSSINGS))
					{
						stat = appendPortals(apexIndex, leftIndex, portalLeft, path,
											 straightPath, straightPathFlags, straightPathRefs,
											 straightPathCount, maxStraightPath, options);
						if (stat != DT_IN_PROGRESS)
							return stat;					
					}
				
					dtVcopy(portalApex, portalLeft);
					apexIndex = leftIndex;
					
					unsigned char flags = 0;
					if (!leftPolyRef)
						flags = DT_STRAIGHTPATH_END;
					else if (leftPolyType == DT_POLYTYPE_OFFMESH_CONNECTION)
						flags = DT_STRAIGHTPATH_OFFMESH_CONNECTION;
					dtPolyRef ref = leftPolyRef;
					
					
					stat = appendVertex(portalApex, flags, ref,
										straightPath, straightPathFlags, straightPathRefs,
										straightPathCount, maxStraightPath);
					if (stat != DT_IN_PROGRESS)
						return stat;
					
					dtVcopy(portalLeft, portalApex);
					dtVcopy(portalRight, portalApex);
					leftIndex = apexIndex;
					rightIndex = apexIndex;
					
					
					i = apexIndex;
					
					continue;
				}
			}
			
			
			if (dtTriArea2D(portalApex, portalLeft, left) >= 0.0f)
			{
				if (dtVequal(portalApex, portalLeft) || dtTriArea2D(portalApex, portalRight, left) < 0.0f)
				{
					dtVcopy(portalLeft, left);
					leftPolyRef = (i+1 < pathSize) ? path[i+1] : 0;
					leftPolyType = toType;
					leftIndex = i;
				}
				else
				{
					
					if (options & (DT_STRAIGHTPATH_AREA_CROSSINGS | DT_STRAIGHTPATH_ALL_CROSSINGS))
					{
						stat = appendPortals(apexIndex, rightIndex, portalRight, path,
											 straightPath, straightPathFlags, straightPathRefs,
											 straightPathCount, maxStraightPath, options);
						if (stat != DT_IN_PROGRESS)
							return stat;
					}

					dtVcopy(portalApex, portalRight);
					apexIndex = rightIndex;
					
					unsigned char flags = 0;
					if (!rightPolyRef)
						flags = DT_STRAIGHTPATH_END;
					else if (rightPolyType == DT_POLYTYPE_OFFMESH_CONNECTION)
						flags = DT_STRAIGHTPATH_OFFMESH_CONNECTION;
					dtPolyRef ref = rightPolyRef;

					
					stat = appendVertex(portalApex, flags, ref,
										straightPath, straightPathFlags, straightPathRefs,
										straightPathCount, maxStraightPath);
					if (stat != DT_IN_PROGRESS)
						return stat;
					
					dtVcopy(portalLeft, portalApex);
					dtVcopy(portalRight, portalApex);
					leftIndex = apexIndex;
					rightIndex = apexIndex;
					
					
					i = apexIndex;
					
					continue;
				}
			}
		}

		
		if (options & (DT_STRAIGHTPATH_AREA_CROSSINGS | DT_STRAIGHTPATH_ALL_CROSSINGS))
		{
			stat = appendPortals(apexIndex, pathSize-1, closestEndPos, path,
								 straightPath, straightPathFlags, straightPathRefs,
								 straightPathCount, maxStraightPath, options);
			if (stat != DT_IN_PROGRESS)
				return stat;
		}
	}

	stat = appendVertex(closestEndPos, DT_STRAIGHTPATH_END, 0,
						straightPath, straightPathFlags, straightPathRefs,
						straightPathCount, maxStraightPath);
	
	return DT_SUCCESS | ((*straightPathCount >= maxStraightPath) ? DT_BUFFER_TOO_SMALL : 0);
}





















dtStatus dtNavMeshQuery::moveAlongSurface(dtPolyRef startRef, const float* startPos, const float* endPos,
										  const dtQueryFilter* filter,
										  float* resultPos, dtPolyRef* visited, int* visitedCount, const int maxVisitedSize) const
{
	(void)( (!!(m_nav)) || (_wassert(L"m_nav", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detournavmeshquery.cpp", 1886), 0) );
	(void)( (!!(m_tinyNodePool)) || (_wassert(L"m_tinyNodePool", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detournavmeshquery.cpp", 1887), 0) );

	*visitedCount = 0;
	
	
	if (!startRef)
		return DT_FAILURE | DT_INVALID_PARAM;
	if (!m_nav->isValidPolyRef(startRef))
		return DT_FAILURE | DT_INVALID_PARAM;
	
	dtStatus status = DT_SUCCESS;
	
	static const int MAX_STACK = 48;
	dtNode* stack[MAX_STACK];
	int nstack = 0;
	
	m_tinyNodePool->clear();
	
	dtNode* startNode = m_tinyNodePool->getNode(startRef);
	startNode->pidx = 0;
	startNode->cost = 0;
	startNode->total = 0;
	startNode->id = startRef;
	startNode->flags = DT_NODE_CLOSED;
	stack[nstack++] = startNode;
	
	float bestPos[3];
	float bestDist = 3.402823466e+38F;
	dtNode* bestNode = 0;
	dtVcopy(bestPos, startPos);
	
	
	float searchPos[3], searchRadSqr;
	dtVlerp(searchPos, startPos, endPos, 0.5f);
	searchRadSqr = dtSqr(dtVdist(startPos, endPos)/2.0f + 0.001f);
	
	float verts[DT_VERTS_PER_POLYGON*3];
	
	while (nstack)
	{
		
		dtNode* curNode = stack[0];
		for (int i = 0; i < nstack-1; ++i)
			stack[i] = stack[i+1];
		nstack--;
		
		
		
		const dtPolyRef curRef = curNode->id;
		const dtMeshTile* curTile = 0;
		const dtPoly* curPoly = 0;
		m_nav->getTileAndPolyByRefUnsafe(curRef, &curTile, &curPoly);			
		
		
		const int nverts = curPoly->vertCount;
		for (int i = 0; i < nverts; ++i)
			dtVcopy(&verts[i*3], &curTile->verts[curPoly->verts[i]*3]);
		
		
		if (dtPointInPolygon(endPos, verts, nverts))
		{
			bestNode = curNode;
			dtVcopy(bestPos, endPos);
			break;
		}
		
		
		for (int i = 0, j = (int)curPoly->vertCount-1; i < (int)curPoly->vertCount; j = i++)
		{
			
			static const int MAX_NEIS = 8;
			int nneis = 0;
			dtPolyRef neis[MAX_NEIS];
			
			if (curPoly->neis[j] & DT_EXT_LINK)
			{
				
				for (unsigned int k = curPoly->firstLink; k != DT_NULL_LINK; k = curTile->links[k].next)
				{
					const dtLink* link = &curTile->links[k];
					if (link->edge == j)
					{
						if (link->ref != 0)
						{
							const dtMeshTile* neiTile = 0;
							const dtPoly* neiPoly = 0;
							m_nav->getTileAndPolyByRefUnsafe(link->ref, &neiTile, &neiPoly);
							if (filter->passFilter(link->ref, neiTile, neiPoly))
							{
								if (nneis < MAX_NEIS)
									neis[nneis++] = link->ref;
							}
						}
					}
				}
			}
			else if (curPoly->neis[j])
			{
				const unsigned int idx = (unsigned int)(curPoly->neis[j]-1);
				const dtPolyRef ref = m_nav->getPolyRefBase(curTile) | idx;
				if (filter->passFilter(ref, curTile, &curTile->polys[idx]))
				{
					
					neis[nneis++] = ref;
				}
			}
			
			if (!nneis)
			{
				
				const float* vj = &verts[j*3];
				const float* vi = &verts[i*3];
				float tseg;
				const float distSqr = dtDistancePtSegSqr2D(endPos, vj, vi, tseg);
				if (distSqr < bestDist)
				{
                    
					dtVlerp(bestPos, vj,vi, tseg);
					bestDist = distSqr;
					bestNode = curNode;
				}
			}
			else
			{
				for (int k = 0; k < nneis; ++k)
				{
					
					dtNode* neighbourNode = m_tinyNodePool->getNode(neis[k]);
					if (!neighbourNode)
						continue;
					
					if (neighbourNode->flags & DT_NODE_CLOSED)
						continue;
					
					
					
					const float* vj = &verts[j*3];
					const float* vi = &verts[i*3];
					float tseg;
					float distSqr = dtDistancePtSegSqr2D(searchPos, vj, vi, tseg);
					if (distSqr > searchRadSqr)
						continue;
					
					
					if (nstack < MAX_STACK)
					{
						neighbourNode->pidx = m_tinyNodePool->getNodeIdx(curNode);
						neighbourNode->flags |= DT_NODE_CLOSED;
						stack[nstack++] = neighbourNode;
					}
				}
			}
		}
	}
	
	int n = 0;
	if (bestNode)
	{
		
		dtNode* prev = 0;
		dtNode* node = bestNode;
		do
		{
			dtNode* next = m_tinyNodePool->getNodeAtIdx(node->pidx);
			node->pidx = m_tinyNodePool->getNodeIdx(prev);
			prev = node;
			node = next;
		}
		while (node);
		
		
		node = prev;
		do
		{
			visited[n++] = node->id;
			if (n >= maxVisitedSize)
			{
				status |= DT_BUFFER_TOO_SMALL;
				break;
			}
			node = m_tinyNodePool->getNodeAtIdx(node->pidx);
		}
		while (node);
	}
	
	dtVcopy(resultPos, bestPos);
	
	*visitedCount = n;
	
	return status;
}


dtStatus dtNavMeshQuery::getPortalPoints(dtPolyRef from, dtPolyRef to, float* left, float* right,
										 unsigned char& fromType, unsigned char& toType) const
{
	(void)( (!!(m_nav)) || (_wassert(L"m_nav", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detournavmeshquery.cpp", 2083), 0) );
	
	const dtMeshTile* fromTile = 0;
	const dtPoly* fromPoly = 0;
	if (dtStatusFailed(m_nav->getTileAndPolyByRef(from, &fromTile, &fromPoly)))
		return DT_FAILURE | DT_INVALID_PARAM;
	fromType = fromPoly->getType();

	const dtMeshTile* toTile = 0;
	const dtPoly* toPoly = 0;
	if (dtStatusFailed(m_nav->getTileAndPolyByRef(to, &toTile, &toPoly)))
		return DT_FAILURE | DT_INVALID_PARAM;
	toType = toPoly->getType();
		
	return getPortalPoints(from, fromPoly, fromTile, to, toPoly, toTile, left, right);
}


dtStatus dtNavMeshQuery::getPortalPoints(dtPolyRef from, const dtPoly* fromPoly, const dtMeshTile* fromTile,
										 dtPolyRef to, const dtPoly* toPoly, const dtMeshTile* toTile,
										 float* left, float* right) const
{
	
	const dtLink* link = 0;
	for (unsigned int i = fromPoly->firstLink; i != DT_NULL_LINK; i = fromTile->links[i].next)
	{
		if (fromTile->links[i].ref == to)
		{
			link = &fromTile->links[i];
			break;
		}
	}
	if (!link)
		return DT_FAILURE | DT_INVALID_PARAM;
	
	
	if (fromPoly->getType() == DT_POLYTYPE_OFFMESH_CONNECTION)
	{
		
		for (unsigned int i = fromPoly->firstLink; i != DT_NULL_LINK; i = fromTile->links[i].next)
		{
			if (fromTile->links[i].ref == to)
			{
				const int v = fromTile->links[i].edge;
				dtVcopy(left, &fromTile->verts[fromPoly->verts[v]*3]);
				dtVcopy(right, &fromTile->verts[fromPoly->verts[v]*3]);
				return DT_SUCCESS;
			}
		}
		return DT_FAILURE | DT_INVALID_PARAM;
	}
	
	if (toPoly->getType() == DT_POLYTYPE_OFFMESH_CONNECTION)
	{
		for (unsigned int i = toPoly->firstLink; i != DT_NULL_LINK; i = toTile->links[i].next)
		{
			if (toTile->links[i].ref == from)
			{
				const int v = toTile->links[i].edge;
				dtVcopy(left, &toTile->verts[toPoly->verts[v]*3]);
				dtVcopy(right, &toTile->verts[toPoly->verts[v]*3]);
				return DT_SUCCESS;
			}
		}
		return DT_FAILURE | DT_INVALID_PARAM;
	}
	
	
	const int v0 = fromPoly->verts[link->edge];
	const int v1 = fromPoly->verts[(link->edge+1) % (int)fromPoly->vertCount];
	dtVcopy(left, &fromTile->verts[v0*3]);
	dtVcopy(right, &fromTile->verts[v1*3]);
	
	
	
	if (link->side != 0xff)
	{
		
		if (link->bmin != 0 || link->bmax != 255)
		{
			const float s = 1.0f/255.0f;
			const float tmin = link->bmin*s;
			const float tmax = link->bmax*s;
			dtVlerp(left, &fromTile->verts[v0*3], &fromTile->verts[v1*3], tmin);
			dtVlerp(right, &fromTile->verts[v0*3], &fromTile->verts[v1*3], tmax);
		}
	}
	
	return DT_SUCCESS;
}


dtStatus dtNavMeshQuery::getEdgeMidPoint(dtPolyRef from, dtPolyRef to, float* mid) const
{
	float left[3], right[3];
	unsigned char fromType, toType;
	if (dtStatusFailed(getPortalPoints(from, to, left,right, fromType, toType)))
		return DT_FAILURE | DT_INVALID_PARAM;
	mid[0] = (left[0]+right[0])*0.5f;
	mid[1] = (left[1]+right[1])*0.5f;
	mid[2] = (left[2]+right[2])*0.5f;
	return DT_SUCCESS;
}

dtStatus dtNavMeshQuery::getEdgeMidPoint(dtPolyRef from, const dtPoly* fromPoly, const dtMeshTile* fromTile,
										 dtPolyRef to, const dtPoly* toPoly, const dtMeshTile* toTile,
										 float* mid) const
{
	float left[3], right[3];
	if (dtStatusFailed(getPortalPoints(from, fromPoly, fromTile, to, toPoly, toTile, left, right)))
		return DT_FAILURE | DT_INVALID_PARAM;
	mid[0] = (left[0]+right[0])*0.5f;
	mid[1] = (left[1]+right[1])*0.5f;
	mid[2] = (left[2]+right[2])*0.5f;
	return DT_SUCCESS;
}







































dtStatus dtNavMeshQuery::raycast(dtPolyRef startRef, const float* startPos, const float* endPos,
								 const dtQueryFilter* filter,
								 float* t, float* hitNormal, dtPolyRef* path, int* pathCount, const int maxPath) const
{
	(void)( (!!(m_nav)) || (_wassert(L"m_nav", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detournavmeshquery.cpp", 2242), 0) );
	
	*t = 0;
	if (pathCount)
		*pathCount = 0;
	
	
	if (!startRef || !m_nav->isValidPolyRef(startRef))
		return DT_FAILURE | DT_INVALID_PARAM;
	
	dtPolyRef curRef = startRef;
	float verts[DT_VERTS_PER_POLYGON*3];	
	int n = 0;
	
	hitNormal[0] = 0;
	hitNormal[1] = 0;
	hitNormal[2] = 0;
	
	dtStatus status = DT_SUCCESS;
	
	while (curRef)
	{
		
		
		
		const dtMeshTile* tile = 0;
		const dtPoly* poly = 0;
		m_nav->getTileAndPolyByRefUnsafe(curRef, &tile, &poly);
		
		
		int nv = 0;
		for (int i = 0; i < (int)poly->vertCount; ++i)
		{
			dtVcopy(&verts[nv*3], &tile->verts[poly->verts[i]*3]);
			nv++;
		}		
		
		float tmin, tmax;
		int segMin, segMax;
		if (!dtIntersectSegmentPoly2D(startPos, endPos, verts, nv, tmin, tmax, segMin, segMax))
		{
			
			if (pathCount)
				*pathCount = n;
			return status;
		}
		
		if (tmax > *t)
			*t = tmax;
		
		
		if (n < maxPath)
			path[n++] = curRef;
		else
			status |= DT_BUFFER_TOO_SMALL;
		
		
		if (segMax == -1)
		{
			*t = 3.402823466e+38F;
			if (pathCount)
				*pathCount = n;
			return status;
		}
		
		
		dtPolyRef nextRef = 0;
		
		for (unsigned int i = poly->firstLink; i != DT_NULL_LINK; i = tile->links[i].next)
		{
			const dtLink* link = &tile->links[i];
			
			
			if ((int)link->edge != segMax)
				continue;
			
			
			const dtMeshTile* nextTile = 0;
			const dtPoly* nextPoly = 0;
			m_nav->getTileAndPolyByRefUnsafe(link->ref, &nextTile, &nextPoly);
			
			
			if (nextPoly->getType() == DT_POLYTYPE_OFFMESH_CONNECTION)
				continue;
			
			
			if (!filter->passFilter(link->ref, nextTile, nextPoly))
				continue;
			
			
			if (link->side == 0xff)
			{
				nextRef = link->ref;
				break;
			}
			
			
			
			
			if (link->bmin == 0 && link->bmax == 255)
			{
				nextRef = link->ref;
				break;
			}
			
			
			const int v0 = poly->verts[link->edge];
			const int v1 = poly->verts[(link->edge+1) % poly->vertCount];
			const float* left = &tile->verts[v0*3];
			const float* right = &tile->verts[v1*3];
			
			
			if (link->side == 0 || link->side == 4)
			{
				
				const float s = 1.0f/255.0f;
				float lmin = left[2] + (right[2] - left[2])*(link->bmin*s);
				float lmax = left[2] + (right[2] - left[2])*(link->bmax*s);
				if (lmin > lmax) dtSwap(lmin, lmax);
				
				
				float z = startPos[2] + (endPos[2]-startPos[2])*tmax;
				if (z >= lmin && z <= lmax)
				{
					nextRef = link->ref;
					break;
				}
			}
			else if (link->side == 2 || link->side == 6)
			{
				
				const float s = 1.0f/255.0f;
				float lmin = left[0] + (right[0] - left[0])*(link->bmin*s);
				float lmax = left[0] + (right[0] - left[0])*(link->bmax*s);
				if (lmin > lmax) dtSwap(lmin, lmax);
				
				
				float x = startPos[0] + (endPos[0]-startPos[0])*tmax;
				if (x >= lmin && x <= lmax)
				{
					nextRef = link->ref;
					break;
				}
			}
		}
		
		if (!nextRef)
		{
			
			
			
			const int a = segMax;
			const int b = segMax+1 < nv ? segMax+1 : 0;
			const float* va = &verts[a*3];
			const float* vb = &verts[b*3];
			const float dx = vb[0] - va[0];
			const float dz = vb[2] - va[2];
			hitNormal[0] = dz;
			hitNormal[1] = 0;
			hitNormal[2] = -dx;
			dtVnormalize(hitNormal);
			
			if (pathCount)
				*pathCount = n;
			return status;
		}
		
		
		curRef = nextRef;
	}
	
	if (pathCount)
		*pathCount = n;
	
	return status;
}






























dtStatus dtNavMeshQuery::findPolysAroundCircle(dtPolyRef startRef, const float* centerPos, const float radius,
											   const dtQueryFilter* filter,
											   dtPolyRef* resultRef, dtPolyRef* resultParent, float* resultCost,
											   int* resultCount, const int maxResult) const
{
	(void)( (!!(m_nav)) || (_wassert(L"m_nav", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detournavmeshquery.cpp", 2453), 0) );
	(void)( (!!(m_nodePool)) || (_wassert(L"m_nodePool", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detournavmeshquery.cpp", 2454), 0) );
	(void)( (!!(m_openList)) || (_wassert(L"m_openList", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detournavmeshquery.cpp", 2455), 0) );

	*resultCount = 0;
	
	
	if (!startRef || !m_nav->isValidPolyRef(startRef))
		return DT_FAILURE | DT_INVALID_PARAM;
	
	m_nodePool->clear();
	m_openList->clear();
	
	dtNode* startNode = m_nodePool->getNode(startRef);
	dtVcopy(startNode->pos, centerPos);
	startNode->pidx = 0;
	startNode->cost = 0;
	startNode->total = 0;
	startNode->id = startRef;
	startNode->flags = DT_NODE_OPEN;
	m_openList->push(startNode);
	
	dtStatus status = DT_SUCCESS;
	
	int n = 0;
	if (n < maxResult)
	{
		if (resultRef)
			resultRef[n] = startNode->id;
		if (resultParent)
			resultParent[n] = 0;
		if (resultCost)
			resultCost[n] = 0;
		++n;
	}
	else
	{
		status |= DT_BUFFER_TOO_SMALL;
	}
	
	const float radiusSqr = dtSqr(radius);
	
	while (!m_openList->empty())
	{
		dtNode* bestNode = m_openList->pop();
		bestNode->flags &= ~DT_NODE_OPEN;
		bestNode->flags |= DT_NODE_CLOSED;
		
		
		
		const dtPolyRef bestRef = bestNode->id;
		const dtMeshTile* bestTile = 0;
		const dtPoly* bestPoly = 0;
		m_nav->getTileAndPolyByRefUnsafe(bestRef, &bestTile, &bestPoly);
		
		
		dtPolyRef parentRef = 0;
		const dtMeshTile* parentTile = 0;
		const dtPoly* parentPoly = 0;
		if (bestNode->pidx)
			parentRef = m_nodePool->getNodeAtIdx(bestNode->pidx)->id;
		if (parentRef)
			m_nav->getTileAndPolyByRefUnsafe(parentRef, &parentTile, &parentPoly);
		
		for (unsigned int i = bestPoly->firstLink; i != DT_NULL_LINK; i = bestTile->links[i].next)
		{
			const dtLink* link = &bestTile->links[i];
			dtPolyRef neighbourRef = link->ref;
			
			if (!neighbourRef || neighbourRef == parentRef)
				continue;
			
			
			const dtMeshTile* neighbourTile = 0;
			const dtPoly* neighbourPoly = 0;
			m_nav->getTileAndPolyByRefUnsafe(neighbourRef, &neighbourTile, &neighbourPoly);
		
			
			if (!filter->passFilter(neighbourRef, neighbourTile, neighbourPoly))
				continue;
			
			
			float va[3], vb[3];
			if (!getPortalPoints(bestRef, bestPoly, bestTile, neighbourRef, neighbourPoly, neighbourTile, va, vb))
				continue;
			
			
			float tseg;
			float distSqr = dtDistancePtSegSqr2D(centerPos, va, vb, tseg);
			if (distSqr > radiusSqr)
				continue;
			
			dtNode* neighbourNode = m_nodePool->getNode(neighbourRef);
			if (!neighbourNode)
			{
				status |= DT_OUT_OF_NODES;
				continue;
			}
				
			if (neighbourNode->flags & DT_NODE_CLOSED)
				continue;
			
			
			if (neighbourNode->flags == 0)
				dtVlerp(neighbourNode->pos, va, vb, 0.5f);
			
			const float total = bestNode->total + dtVdist(bestNode->pos, neighbourNode->pos);
			
			
			if ((neighbourNode->flags & DT_NODE_OPEN) && total >= neighbourNode->total)
				continue;
			
			neighbourNode->id = neighbourRef;
			neighbourNode->flags = (neighbourNode->flags & ~DT_NODE_CLOSED);
			neighbourNode->pidx = m_nodePool->getNodeIdx(bestNode);
			neighbourNode->total = total;
			
			if (neighbourNode->flags & DT_NODE_OPEN)
			{
				m_openList->modify(neighbourNode);
			}
			else
			{
				if (n < maxResult)
				{
					if (resultRef)
						resultRef[n] = neighbourNode->id;
					if (resultParent)
						resultParent[n] = m_nodePool->getNodeAtIdx(neighbourNode->pidx)->id;
					if (resultCost)
						resultCost[n] = neighbourNode->total;
					++n;
				}
				else
				{
					status |= DT_BUFFER_TOO_SMALL;
				}
				neighbourNode->flags = DT_NODE_OPEN;
				m_openList->push(neighbourNode);
			}
		}
	}
	
	*resultCount = n;
	
	return status;
}























dtStatus dtNavMeshQuery::findPolysAroundShape(dtPolyRef startRef, const float* verts, const int nverts,
											  const dtQueryFilter* filter,
											  dtPolyRef* resultRef, dtPolyRef* resultParent, float* resultCost,
											  int* resultCount, const int maxResult) const
{
	(void)( (!!(m_nav)) || (_wassert(L"m_nav", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detournavmeshquery.cpp", 2628), 0) );
	(void)( (!!(m_nodePool)) || (_wassert(L"m_nodePool", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detournavmeshquery.cpp", 2629), 0) );
	(void)( (!!(m_openList)) || (_wassert(L"m_openList", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detournavmeshquery.cpp", 2630), 0) );
	
	*resultCount = 0;
	
	
	if (!startRef || !m_nav->isValidPolyRef(startRef))
		return DT_FAILURE | DT_INVALID_PARAM;
	
	m_nodePool->clear();
	m_openList->clear();
	
	float centerPos[3] = {0,0,0};
	for (int i = 0; i < nverts; ++i)
		dtVadd(centerPos,centerPos,&verts[i*3]);
	dtVscale(centerPos,centerPos,1.0f/nverts);

	dtNode* startNode = m_nodePool->getNode(startRef);
	dtVcopy(startNode->pos, centerPos);
	startNode->pidx = 0;
	startNode->cost = 0;
	startNode->total = 0;
	startNode->id = startRef;
	startNode->flags = DT_NODE_OPEN;
	m_openList->push(startNode);
	
	dtStatus status = DT_SUCCESS;

	int n = 0;
	if (n < maxResult)
	{
		if (resultRef)
			resultRef[n] = startNode->id;
		if (resultParent)
			resultParent[n] = 0;
		if (resultCost)
			resultCost[n] = 0;
		++n;
	}
	else
	{
		status |= DT_BUFFER_TOO_SMALL;
	}
	
	while (!m_openList->empty())
	{
		dtNode* bestNode = m_openList->pop();
		bestNode->flags &= ~DT_NODE_OPEN;
		bestNode->flags |= DT_NODE_CLOSED;
		
		
		
		const dtPolyRef bestRef = bestNode->id;
		const dtMeshTile* bestTile = 0;
		const dtPoly* bestPoly = 0;
		m_nav->getTileAndPolyByRefUnsafe(bestRef, &bestTile, &bestPoly);
		
		
		dtPolyRef parentRef = 0;
		const dtMeshTile* parentTile = 0;
		const dtPoly* parentPoly = 0;
		if (bestNode->pidx)
			parentRef = m_nodePool->getNodeAtIdx(bestNode->pidx)->id;
		if (parentRef)
			m_nav->getTileAndPolyByRefUnsafe(parentRef, &parentTile, &parentPoly);
		
		for (unsigned int i = bestPoly->firstLink; i != DT_NULL_LINK; i = bestTile->links[i].next)
		{
			const dtLink* link = &bestTile->links[i];
			dtPolyRef neighbourRef = link->ref;
			
			if (!neighbourRef || neighbourRef == parentRef)
				continue;
			
			
			const dtMeshTile* neighbourTile = 0;
			const dtPoly* neighbourPoly = 0;
			m_nav->getTileAndPolyByRefUnsafe(neighbourRef, &neighbourTile, &neighbourPoly);
			
			
			if (!filter->passFilter(neighbourRef, neighbourTile, neighbourPoly))
				continue;
			
			
			float va[3], vb[3];
			if (!getPortalPoints(bestRef, bestPoly, bestTile, neighbourRef, neighbourPoly, neighbourTile, va, vb))
				continue;
			
			
			float tmin, tmax;
			int segMin, segMax;
			if (!dtIntersectSegmentPoly2D(va, vb, verts, nverts, tmin, tmax, segMin, segMax))
				continue;
			if (tmin > 1.0f || tmax < 0.0f)
				continue;
			
			dtNode* neighbourNode = m_nodePool->getNode(neighbourRef);
			if (!neighbourNode)
			{
				status |= DT_OUT_OF_NODES;
				continue;
			}
			
			if (neighbourNode->flags & DT_NODE_CLOSED)
				continue;
			
			
			if (neighbourNode->flags == 0)
				dtVlerp(neighbourNode->pos, va, vb, 0.5f);
			
			const float total = bestNode->total + dtVdist(bestNode->pos, neighbourNode->pos);
			
			
			if ((neighbourNode->flags & DT_NODE_OPEN) && total >= neighbourNode->total)
				continue;
			
			neighbourNode->id = neighbourRef;
			neighbourNode->flags = (neighbourNode->flags & ~DT_NODE_CLOSED);
			neighbourNode->pidx = m_nodePool->getNodeIdx(bestNode);
			neighbourNode->total = total;
			
			if (neighbourNode->flags & DT_NODE_OPEN)
			{
				m_openList->modify(neighbourNode);
			}
			else
			{
				if (n < maxResult)
				{
					if (resultRef)
						resultRef[n] = neighbourNode->id;
					if (resultParent)
						resultParent[n] = m_nodePool->getNodeAtIdx(neighbourNode->pidx)->id;
					if (resultCost)
						resultCost[n] = neighbourNode->total;
					++n;
				}
				else
				{
					status |= DT_BUFFER_TOO_SMALL;
				}
				neighbourNode->flags = DT_NODE_OPEN;
				m_openList->push(neighbourNode);
			}
		}
	}
	
	*resultCount = n;
	
	return status;
}























dtStatus dtNavMeshQuery::findLocalNeighbourhood(dtPolyRef startRef, const float* centerPos, const float radius,
												const dtQueryFilter* filter,
												dtPolyRef* resultRef, dtPolyRef* resultParent,
												int* resultCount, const int maxResult) const
{
	(void)( (!!(m_nav)) || (_wassert(L"m_nav", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detournavmeshquery.cpp", 2808), 0) );
	(void)( (!!(m_tinyNodePool)) || (_wassert(L"m_tinyNodePool", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detournavmeshquery.cpp", 2809), 0) );
	
	*resultCount = 0;

	
	if (!startRef || !m_nav->isValidPolyRef(startRef))
		return DT_FAILURE | DT_INVALID_PARAM;
	
	static const int MAX_STACK = 48;
	dtNode* stack[MAX_STACK];
	int nstack = 0;
	
	m_tinyNodePool->clear();
	
	dtNode* startNode = m_tinyNodePool->getNode(startRef);
	startNode->pidx = 0;
	startNode->id = startRef;
	startNode->flags = DT_NODE_CLOSED;
	stack[nstack++] = startNode;
	
	const float radiusSqr = dtSqr(radius);
	
	float pa[DT_VERTS_PER_POLYGON*3];
	float pb[DT_VERTS_PER_POLYGON*3];
	
	dtStatus status = DT_SUCCESS;
	
	int n = 0;
	if (n < maxResult)
	{
		resultRef[n] = startNode->id;
		if (resultParent)
			resultParent[n] = 0;
		++n;
	}
	else
	{
		status |= DT_BUFFER_TOO_SMALL;
	}
	
	while (nstack)
	{
		
		dtNode* curNode = stack[0];
		for (int i = 0; i < nstack-1; ++i)
			stack[i] = stack[i+1];
		nstack--;
		
		
		
		const dtPolyRef curRef = curNode->id;
		const dtMeshTile* curTile = 0;
		const dtPoly* curPoly = 0;
		m_nav->getTileAndPolyByRefUnsafe(curRef, &curTile, &curPoly);
		
		for (unsigned int i = curPoly->firstLink; i != DT_NULL_LINK; i = curTile->links[i].next)
		{
			const dtLink* link = &curTile->links[i];
			dtPolyRef neighbourRef = link->ref;
			
			if (!neighbourRef)
				continue;
			
			
			dtNode* neighbourNode = m_tinyNodePool->getNode(neighbourRef);
			if (!neighbourNode)
				continue;
			
			if (neighbourNode->flags & DT_NODE_CLOSED)
				continue;
			
			
			const dtMeshTile* neighbourTile = 0;
			const dtPoly* neighbourPoly = 0;
			m_nav->getTileAndPolyByRefUnsafe(neighbourRef, &neighbourTile, &neighbourPoly);
			
			
			if (neighbourPoly->getType() == DT_POLYTYPE_OFFMESH_CONNECTION)
				continue;
			
			
			if (!filter->passFilter(neighbourRef, neighbourTile, neighbourPoly))
				continue;
			
			
			float va[3], vb[3];
			if (!getPortalPoints(curRef, curPoly, curTile, neighbourRef, neighbourPoly, neighbourTile, va, vb))
				continue;
			
			
			float tseg;
			float distSqr = dtDistancePtSegSqr2D(centerPos, va, vb, tseg);
			if (distSqr > radiusSqr)
				continue;
			
			
			
			neighbourNode->flags |= DT_NODE_CLOSED;
			neighbourNode->pidx = m_tinyNodePool->getNodeIdx(curNode);
			
			
			
			
			const int npa = neighbourPoly->vertCount;
			for (int k = 0; k < npa; ++k)
				dtVcopy(&pa[k*3], &neighbourTile->verts[neighbourPoly->verts[k]*3]);
			
			bool overlap = false;
			for (int j = 0; j < n; ++j)
			{
				dtPolyRef pastRef = resultRef[j];
				
				
				bool connected = false;
				for (unsigned int k = curPoly->firstLink; k != DT_NULL_LINK; k = curTile->links[k].next)
				{
					if (curTile->links[k].ref == pastRef)
					{
						connected = true;
						break;
					}
				}
				if (connected)
					continue;
				
				
				const dtMeshTile* pastTile = 0;
				const dtPoly* pastPoly = 0;
				m_nav->getTileAndPolyByRefUnsafe(pastRef, &pastTile, &pastPoly);
				
				
				const int npb = pastPoly->vertCount;
				for (int k = 0; k < npb; ++k)
					dtVcopy(&pb[k*3], &pastTile->verts[pastPoly->verts[k]*3]);
				
				if (dtOverlapPolyPoly2D(pa,npa, pb,npb))
				{
					overlap = true;
					break;
				}
			}
			if (overlap)
				continue;
			
			
			if (n < maxResult)
			{
				resultRef[n] = neighbourRef;
				if (resultParent)
					resultParent[n] = curRef;
				++n;
			}
			else
			{
				status |= DT_BUFFER_TOO_SMALL;
			}
			
			if (nstack < MAX_STACK)
			{
				stack[nstack++] = neighbourNode;
			}
		}
	}
	
	*resultCount = n;
	
	return status;
}


struct dtSegInterval
{
	dtPolyRef ref;
	short tmin, tmax;
};

static void insertInterval(dtSegInterval* ints, int& nints, const int maxInts,
						   const short tmin, const short tmax, const dtPolyRef ref)
{
	if (nints+1 > maxInts) return;
	
	int idx = 0;
	while (idx < nints)
	{
		if (tmax <= ints[idx].tmin)
			break;
		idx++;
	}
	
	if (nints-idx)
		memmove(ints+idx+1, ints+idx, sizeof(dtSegInterval)*(nints-idx));
	
	ints[idx].ref = ref;
	ints[idx].tmin = tmin;
	ints[idx].tmax = tmax;
	nints++;
}












dtStatus dtNavMeshQuery::getPolyWallSegments(dtPolyRef ref, const dtQueryFilter* filter,
											 float* segmentVerts, dtPolyRef* segmentRefs, int* segmentCount,
											 const int maxSegments) const
{
	(void)( (!!(m_nav)) || (_wassert(L"m_nav", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detournavmeshquery.cpp", 3022), 0) );
	
	*segmentCount = 0;
	
	const dtMeshTile* tile = 0;
	const dtPoly* poly = 0;
	if (dtStatusFailed(m_nav->getTileAndPolyByRef(ref, &tile, &poly)))
		return DT_FAILURE | DT_INVALID_PARAM;
	
	int n = 0;
	static const int MAX_INTERVAL = 16;
	dtSegInterval ints[MAX_INTERVAL];
	int nints;
	
	const bool storePortals = segmentRefs != 0;
	
	dtStatus status = DT_SUCCESS;
	
	for (int i = 0, j = (int)poly->vertCount-1; i < (int)poly->vertCount; j = i++)
	{
		
		nints = 0;
		if (poly->neis[j] & DT_EXT_LINK)
		{
			
			for (unsigned int k = poly->firstLink; k != DT_NULL_LINK; k = tile->links[k].next)
			{
				const dtLink* link = &tile->links[k];
				if (link->edge == j)
				{
					if (link->ref != 0)
					{
						const dtMeshTile* neiTile = 0;
						const dtPoly* neiPoly = 0;
						m_nav->getTileAndPolyByRefUnsafe(link->ref, &neiTile, &neiPoly);
						if (filter->passFilter(link->ref, neiTile, neiPoly))
						{
							insertInterval(ints, nints, MAX_INTERVAL, link->bmin, link->bmax, link->ref);
						}
					}
				}
			}
		}
		else
		{
			
			dtPolyRef neiRef = 0;
			if (poly->neis[j])
			{
				const unsigned int idx = (unsigned int)(poly->neis[j]-1);
				neiRef = m_nav->getPolyRefBase(tile) | idx;
				if (!filter->passFilter(neiRef, tile, &tile->polys[idx]))
					neiRef = 0;
			}

			
			if (neiRef != 0 && !storePortals)
				continue;
			
			if (n < maxSegments)
			{
				const float* vj = &tile->verts[poly->verts[j]*3];
				const float* vi = &tile->verts[poly->verts[i]*3];
				float* seg = &segmentVerts[n*6];
				dtVcopy(seg+0, vj);
				dtVcopy(seg+3, vi);
				if (segmentRefs)
					segmentRefs[n] = neiRef;
				n++;
			}
			else
			{
				status |= DT_BUFFER_TOO_SMALL;
			}
			
			continue;
		}
		
		
		insertInterval(ints, nints, MAX_INTERVAL, -1, 0, 0);
		insertInterval(ints, nints, MAX_INTERVAL, 255, 256, 0);
		
		
		const float* vj = &tile->verts[poly->verts[j]*3];
		const float* vi = &tile->verts[poly->verts[i]*3];
		for (int k = 1; k < nints; ++k)
		{
			
			if (storePortals && ints[k].ref)
			{
				const float tmin = ints[k].tmin/255.0f; 
				const float tmax = ints[k].tmax/255.0f; 
				if (n < maxSegments)
				{
					float* seg = &segmentVerts[n*6];
					dtVlerp(seg+0, vj,vi, tmin);
					dtVlerp(seg+3, vj,vi, tmax);
					if (segmentRefs)
						segmentRefs[n] = ints[k].ref;
					n++;
				}
				else
				{
					status |= DT_BUFFER_TOO_SMALL;
				}
			}

			
			const int imin = ints[k-1].tmax;
			const int imax = ints[k].tmin;
			if (imin != imax)
			{
				const float tmin = imin/255.0f; 
				const float tmax = imax/255.0f; 
				if (n < maxSegments)
				{
					float* seg = &segmentVerts[n*6];
					dtVlerp(seg+0, vj,vi, tmin);
					dtVlerp(seg+3, vj,vi, tmax);
					if (segmentRefs)
						segmentRefs[n] = 0;
					n++;
				}
				else
				{
					status |= DT_BUFFER_TOO_SMALL;
				}
			}
		}
	}
	
	*segmentCount = n;
	
	return status;
}











dtStatus dtNavMeshQuery::findDistanceToWall(dtPolyRef startRef, const float* centerPos, const float maxRadius,
											const dtQueryFilter* filter,
											float* hitDist, float* hitPos, float* hitNormal) const
{
	(void)( (!!(m_nav)) || (_wassert(L"m_nav", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detournavmeshquery.cpp", 3172), 0) );
	(void)( (!!(m_nodePool)) || (_wassert(L"m_nodePool", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detournavmeshquery.cpp", 3173), 0) );
	(void)( (!!(m_openList)) || (_wassert(L"m_openList", L"f:\\mygit\\fwcyclehero\\server\\src\\lib\\navigation\\detournavmeshquery.cpp", 3174), 0) );
	
	
	if (!startRef || !m_nav->isValidPolyRef(startRef))
		return DT_FAILURE | DT_INVALID_PARAM;
	
	m_nodePool->clear();
	m_openList->clear();
	
	dtNode* startNode = m_nodePool->getNode(startRef);
	dtVcopy(startNode->pos, centerPos);
	startNode->pidx = 0;
	startNode->cost = 0;
	startNode->total = 0;
	startNode->id = startRef;
	startNode->flags = DT_NODE_OPEN;
	m_openList->push(startNode);
	
	float radiusSqr = dtSqr(maxRadius);
	
	dtStatus status = DT_SUCCESS;
	
	while (!m_openList->empty())
	{
		dtNode* bestNode = m_openList->pop();
		bestNode->flags &= ~DT_NODE_OPEN;
		bestNode->flags |= DT_NODE_CLOSED;
		
		
		
		const dtPolyRef bestRef = bestNode->id;
		const dtMeshTile* bestTile = 0;
		const dtPoly* bestPoly = 0;
		m_nav->getTileAndPolyByRefUnsafe(bestRef, &bestTile, &bestPoly);
		
		
		dtPolyRef parentRef = 0;
		const dtMeshTile* parentTile = 0;
		const dtPoly* parentPoly = 0;
		if (bestNode->pidx)
			parentRef = m_nodePool->getNodeAtIdx(bestNode->pidx)->id;
		if (parentRef)
			m_nav->getTileAndPolyByRefUnsafe(parentRef, &parentTile, &parentPoly);
		
		
		for (int i = 0, j = (int)bestPoly->vertCount-1; i < (int)bestPoly->vertCount; j = i++)
		{
			
			if (bestPoly->neis[j] & DT_EXT_LINK)
			{
				
				bool solid = true;
				for (unsigned int k = bestPoly->firstLink; k != DT_NULL_LINK; k = bestTile->links[k].next)
				{
					const dtLink* link = &bestTile->links[k];
					if (link->edge == j)
					{
						if (link->ref != 0)
						{
							const dtMeshTile* neiTile = 0;
							const dtPoly* neiPoly = 0;
							m_nav->getTileAndPolyByRefUnsafe(link->ref, &neiTile, &neiPoly);
							if (filter->passFilter(link->ref, neiTile, neiPoly))
								solid = false;
						}
						break;
					}
				}
				if (!solid) continue;
			}
			else if (bestPoly->neis[j])
			{
				
				const unsigned int idx = (unsigned int)(bestPoly->neis[j]-1);
				const dtPolyRef ref = m_nav->getPolyRefBase(bestTile) | idx;
				if (filter->passFilter(ref, bestTile, &bestTile->polys[idx]))
					continue;
			}
			
			
			const float* vj = &bestTile->verts[bestPoly->verts[j]*3];
			const float* vi = &bestTile->verts[bestPoly->verts[i]*3];
			float tseg;
			float distSqr = dtDistancePtSegSqr2D(centerPos, vj, vi, tseg);
			
			
			if (distSqr > radiusSqr)
				continue;
			
			
			radiusSqr = distSqr;
			
			hitPos[0] = vj[0] + (vi[0] - vj[0])*tseg;
			hitPos[1] = vj[1] + (vi[1] - vj[1])*tseg;
			hitPos[2] = vj[2] + (vi[2] - vj[2])*tseg;
		}
		
		for (unsigned int i = bestPoly->firstLink; i != DT_NULL_LINK; i = bestTile->links[i].next)
		{
			const dtLink* link = &bestTile->links[i];
			dtPolyRef neighbourRef = link->ref;
			
			if (!neighbourRef || neighbourRef == parentRef)
				continue;
			
			
			const dtMeshTile* neighbourTile = 0;
			const dtPoly* neighbourPoly = 0;
			m_nav->getTileAndPolyByRefUnsafe(neighbourRef, &neighbourTile, &neighbourPoly);
			
			
			if (neighbourPoly->getType() == DT_POLYTYPE_OFFMESH_CONNECTION)
				continue;
			
			
			const float* va = &bestTile->verts[bestPoly->verts[link->edge]*3];
			const float* vb = &bestTile->verts[bestPoly->verts[(link->edge+1) % bestPoly->vertCount]*3];
			float tseg;
			float distSqr = dtDistancePtSegSqr2D(centerPos, va, vb, tseg);
			
			
			if (distSqr > radiusSqr)
				continue;
			
			if (!filter->passFilter(neighbourRef, neighbourTile, neighbourPoly))
				continue;

			dtNode* neighbourNode = m_nodePool->getNode(neighbourRef);
			if (!neighbourNode)
			{
				status |= DT_OUT_OF_NODES;
				continue;
			}
			
			if (neighbourNode->flags & DT_NODE_CLOSED)
				continue;
			
			
			if (neighbourNode->flags == 0)
			{
				getEdgeMidPoint(bestRef, bestPoly, bestTile,
								neighbourRef, neighbourPoly, neighbourTile, neighbourNode->pos);
			}
			
			const float total = bestNode->total + dtVdist(bestNode->pos, neighbourNode->pos);
			
			
			if ((neighbourNode->flags & DT_NODE_OPEN) && total >= neighbourNode->total)
				continue;
			
			neighbourNode->id = neighbourRef;
			neighbourNode->flags = (neighbourNode->flags & ~DT_NODE_CLOSED);
			neighbourNode->pidx = m_nodePool->getNodeIdx(bestNode);
			neighbourNode->total = total;
				
			if (neighbourNode->flags & DT_NODE_OPEN)
			{
				m_openList->modify(neighbourNode);
			}
			else
			{
				neighbourNode->flags |= DT_NODE_OPEN;
				m_openList->push(neighbourNode);
			}
		}
	}
	
	
	dtVsub(hitNormal, centerPos, hitPos);
	dtVnormalize(hitNormal);
	
	*hitDist = sqrtf(radiusSqr);
	
	return status;
}

bool dtNavMeshQuery::isValidPolyRef(dtPolyRef ref, const dtQueryFilter* filter) const
{
	const dtMeshTile* tile = 0;
	const dtPoly* poly = 0;
	dtStatus status = m_nav->getTileAndPolyByRef(ref, &tile, &poly);
	
	if (dtStatusFailed(status))
		return false;
	
	if (!filter->passFilter(ref, tile, poly))
		return false;
	return true;
}






bool dtNavMeshQuery::isInClosedList(dtPolyRef ref) const
{
	if (!m_nodePool) return false;
	const dtNode* node = m_nodePool->findNode(ref);
	return node && node->flags & DT_NODE_CLOSED;
}

