1	SQL - БАЗОВЫЕ ЗНАНИЯ  


1.1	Вопрос на знание select … join  
Есть две таблицы:  
Т1 (ID int, Text1 …., text2 …. B и т.д.)  
И   
Т2 (ID int, Text1 …., text2 …. B и т.д.)  

Написать select
1.	Вывести все поля из обеих таблиц, вывести записи при условии, что ID обеих таблиц совпадают.
2.	Вывести все поля из обеих таблиц, вывести все записи из T1 и только имеющиеся в T2
3.	Вывести все записи из T1, при условии, что таких ID нет в T2


Ответ:  
1.   
```sql
SELECT
    t1.*,
    t2.*
FROM T1 AS t1
INNER JOIN T2 AS t2
    ON t1.ID = t2.ID;
```

2.  
```sql
SELECT
    t1.*,
    t2.*
FROM T1 AS t1
LEFT JOIN T2 AS t2
    ON t1.ID = t2.ID;
```

3.  
```sql
SELECT
    t1.*
FROM T1 AS t1
LEFT JOIN T2 AS t2
    ON t1.ID = t2.ID
WHERE t2.ID IS NULL;
```

1.2	Как вывести результат запроса в XML?  


Пусть есть таблица T со следующим видом и содержанием. Что вернет SQL запрос?  
Id	Code	Name	StatusId  
1	gargadgadfga	Запрос предложений 1	45  
2	bsftrggdfgadfgdfat	Запрос предложений 2	2  
3	gfadgdfsgdfsgs	Запрос предложений 3	45  
4	afgereaerffdgvdf	Запрос предложений 4	3  
5	dgadfterdsgsdgad	Запрос предложений 5	45  
6	argrgag	Запрос предложений 6	2  

Написать запрос, выводящий данные в XML  

Ответ:  
```sql
SELECT 
    Id,
    Code,
    Name,
    StatusId
FROM T
FOR XML AUTO;
```


1.3	Как выбрать данные из поля с XML?  
Написать запрос, выбирающий данные из XML из предыдущего вопроса  
Отфильтровать данные по StatusId != 3  

```sql
SELECT
    x.value('(Id/text())[1]', 'INT')       AS Id,
    x.value('(Code/text())[1]', 'NVARCHAR(100)') AS Code,
    x.value('(Name/text())[1]', 'NVARCHAR(100)') AS Name,
    x.value('(StatusId/text())[1]', 'INT') AS StatusId
FROM T_XML
CROSS APPLY XmlData.nodes('/rows/row') AS t(x)
WHERE x.value('(StatusId/text())[1]', 'INT') != 3;
```


1.4	Что такое hints

Отве: 
