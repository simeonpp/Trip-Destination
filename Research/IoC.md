#Inversion of control (IoC) Reseach

## Ninject vs Autofac

### Source 1
All times are in ms
![IoC: Ninject vs Autofac](http://i.imgur.com/Ic9Cqtb.png)

Source:
[http://cardinalcore.co.uk/2015/01/28/ioc-battle-in-2015-results-using-ninject-think-again/](http://cardinalcore.co.uk/2015/01/28/ioc-battle-in-2015-results-using-ninject-think-again/ "IoC Battle in 2015")

### Source 2
Every interface is resolved 500.000 times during the benchmark and the time is measured in milliseconds. Each test is executed single threaded and multi threaded.

**The benchmarks: **


- Singleton: Objects with is singleton lifetime are resolved
- Transient: Objects with is transient lifetime are resolved
- Combined: Objects with two dependencies (singleton and transient lifetime) are resolved
- Complex: Objects with several nested dependencies are resolved
- Property: Objects which require property injection are resolved
- Generics: Objects with a generic dependency are resolved
- IEnumerable: Several objects that implement the same interface are resolved
- Conditional: Objects with a conditional dependency are resolved
- Child Container: Objects are resolved trough a child container
- Interception With Proxy: Objects with a dynamically generated wrapper are resolved
- Prepare And Register: Initializes container and registers some basic elements (executed 3.000 times)
- Prepare And Register And Simple Resolve: Initializes container and registers some basic elements. The resolves two objects (executed 3.000 times)




![IoC: Benchmark](http://i.imgur.com/VuJ8VLB.png)

Source: [http://www.palmmedia.de/blog/2011/8/30/ioc-container-benchmark-performance-comparison](http://www.palmmedia.de/blog/2011/8/30/ioc-container-benchmark-performance-comparison "IoC Benchmark")