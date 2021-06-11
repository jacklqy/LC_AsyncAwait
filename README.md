# LC_AsyncAwait
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


