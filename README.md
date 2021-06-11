# LC_AsyncAwait
用同步的方式写异步回调

## await/async语法
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
