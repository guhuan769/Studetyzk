## 目录 
 [异步方法解析](#异步方法解析) 
# 异步方法解析
![image](https://user-images.githubusercontent.com/46043439/205428691-615c2049-473c-4ee8-8f58-addd9d30b592.png)  
![image](https://user-images.githubusercontent.com/46043439/205429178-18e8bfc5-e6f2-4499-a813-25b21ad8171b.png)  
如:像WinFrom中Button按钮事件 由于委托的固定返回值 所以不支持异步 那么又非要用异步应该怎么处理呢 只需要在异步方法.Result()读值即可解决
![image](https://user-images.githubusercontent.com/46043439/205429345-c59af2ec-719e-4f2f-8184-5484340f8edf.png)  
不支持异步 就可以手动的拿异步方法的值
![image](https://user-images.githubusercontent.com/46043439/205429471-6df262f6-4dbf-46aa-988e-0a00f3f8fde1.png)
那么如果我想写入值的话可以用wait()
![image](https://user-images.githubusercontent.com/46043439/205429529-3c3a124c-113e-4979-b10d-f46a3ab40edd.png)  
重：虽然这样能解决异步 但是各位经量不要去调用wait()与Result().因为有几率造成死锁的风险  那么最妥善的处理放:  
线程池TheadPool(守护线程)  
异步委托  
![image](https://user-images.githubusercontent.com/46043439/205429803-0fadcc95-6f92-455f-b191-78a505be4c31.png)  
![image](https://user-images.githubusercontent.com/46043439/205429891-3f00eb59-7a9f-4f95-9039-13742f6950f3.png)
async await原理
![image](https://user-images.githubusercontent.com/46043439/205433777-27821b2a-57c0-4669-8450-20048269dd01.png)  

1 Thread.CurrentThead.ManagedThreadid获得当前线程ID 。验证：在耗时异步(写入大写字符串)操作前后分别打印线程ID  


