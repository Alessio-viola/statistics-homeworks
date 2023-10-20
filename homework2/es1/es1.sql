--Qualitative variable
SELECT LeaderOrPlayer, COUNT(*) AS absolute_frequency, 
       COUNT(*) / (SELECT COUNT(*) FROM dataset) * 100 AS percentage_frequency
FROM dataset
GROUP BY LeaderOrPlayer;

--Quantitative discrete variable 
SELECT HardWorker, COUNT(*) AS absolute_frequency,
       COUNT(*) / (SELECT COUNT(*) FROM dataset) * 100 AS percentage_frequency
FROM dataset
GROUP BY HardWorker;

--joint distribution
SELECT LeaderOrPlayer, HardWorker, COUNT(*) AS joint_frequency
FROM dataset
GROUP BY LeaderOrPlayer, HardWorker;

