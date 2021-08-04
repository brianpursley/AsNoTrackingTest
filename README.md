# AsNoTrackingTest

Curious behavior seen when benchmarking repeated execution of a simple query with and without `.AsNoTracking()`.

### Running without `.AsNoTracking()` 
10,000 iterations takes approx **5.1 seconds**:
```
$ time ./AsNoTrackingTest false 10000
useAsNoTracking = False, iterations = 10000

real	0m5.144s
user	0m4.822s
sys	0m0.433s
```

### Running with `.AsNoTracking()` 
10,000 iterations takes approx **9.6 seconds**:
```
$ time ./AsNoTrackingTest true 10000
useAsNoTracking = True, iterations = 10000

real	0m9.638s
user	0m9.316s
sys	0m0.445s
```

---

Using .NET version 5.0.302 on Ubuntu 20.04.2, Kernel Version 5.8.0.63
```
$ dotnet --version
5.0.302
$ lsb_release -d
Description:	Ubuntu 20.04.2 LTS
$ uname -a
Linux cinlogic-xps13 5.8.0-63-generic #71~20.04.1-Ubuntu SMP Thu Jul 15 17:46:08 UTC 2021 x86_64 x86_64 x86_64 GNU/Linux

```