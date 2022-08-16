# UDevMe.CarSupplier

<p>Реализация алгоритма распределения авто. В данном случае только поступление.</p>

<p>
Добавление авто происходит по следующему принципу:
  <ol>
    <li>Если авто одной марки и цвета поровну в автосалонах, распределение новой 50% вероятности</li>
    <li>Если разница 1 авто, то вероятностью 66,6% распределяем в меньший автосалон</li>
    <li>Если разница 2 авто, то вероятностью 80% распределяем в меньший автосалон</li>
    <li>Если разница 3 авто, то вероятностью 100% распределяем в меньший автосалон</li>
  </ol>
</p>

<p>
  Ограничения:
  </br>
  У каждого автосалона есть ограниченное количество мест для авто.
</p>

## Пример запуска
![alt text](https://github.com/val-ugs/UDevMe.CarSupplier/blob/master/CarSupplierImage.png?raw=true)