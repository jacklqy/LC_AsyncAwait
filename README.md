# LC_AsyncAwait
用同步的方式写异步回调

## async/await语法
1  async 是用来修饰方法，如果单独出现，方法会警告,没有什么作用

2  await在方法体内部，只能放在async修饰的方法内，必须放在task前面

3  async/await方法里面如果没有返回值，默认返回一个Task，或者void(推荐用Task，而不是void，因为这样才能await/wait)

4  带async+await后，返回值要多一层Task<>

## 等待结果
t.Wait()

Task.WaitAll()

t.Result

await t

## 同步Sync-》有序执行 | 多线程Task并行执行不阻塞-》无序执行 | 多线程Task.ContinueWith不阻塞异步回调-》有序执行 | 异步async/await 不阻塞-》有序执行
![image](https://user-images.githubusercontent.com/26539681/121616223-11a69600-ca95-11eb-9839-2ec6c6203381.png)

## 探究await/async
Async+Task+一个await：方法体进入了MoveNext，主线程执行
![image](https://user-images.githubusercontent.com/26539681/121616941-7d3d3300-ca96-11eb-80c0-21e106ef2492.png)
![image](https://user-images.githubusercontent.com/26539681/121616632-d22c7980-ca95-11eb-8fca-f55b3cf6a4b4.png)

如果是Async+Task+多个await一步步执行—修改状态—再来新的await，则循环执行完成。

## async/await价值？
提升性能？还是增加吞吐？串行---没有并发---就不提升性能

性能：速度快慢—不能提升性能

吞吐：响应能力，1s能处理多少个请求—Web模式

还有线程协调的成本。。。更浪费? 

### 1、ReadFile对比:Task/Async/Sync
![image](https://user-images.githubusercontent.com/26539681/121619681-91376380-ca9b-11eb-9ab1-b3f5fa5341a2.png)
总结：

Task：当然并发---10个线程

Async: 可以并发，但是并发不多---只有3个线程

Sync：同步，按顺序执行

这里的动作是读硬盘---都是读到当前程序里面，会很卡---不仅耗时而且卡

ReadAllBytesAsync这里的线程呢？ 对不起，这里没有！！！

### 2、InvokeWeb对比:Task/Async/Sync
![image](https://user-images.githubusercontent.com/26539681/121620992-e3798400-ca9d-11eb-9753-e09865d7bbab.png)
总结：

Task：耗时长一些，并发不够高------10个线程---铁打的10个线程

Async：并发高，速度快----少于10个线程，没有影响并发，能重用就是没事儿了，利用率高一些

Sync：串行的，耗时长

其实对电脑负荷比较小，GetResponseAsync这里的线程呢？ 对不起，这里没有！！！

### 3、DoCalculation密集型计算对比:Task/Async/Sync
todo...



