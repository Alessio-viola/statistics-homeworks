# R code for simulation
set.seed(123)

n1 <- 1000
n2 <- 100
n3 <- 50  # Aumentato il numero di campioni per il terzo campione

sample_data1 <- rnorm(n1)
sample_data2 <- rnorm(n2)
sample_data3 <- rnorm(n3)

edf1 <- ecdf(sample_data1)
edf2 <- ecdf(sample_data2)
edf3 <- ecdf(sample_data3)

# Plotting
jpeg("output_image.jpg", width = 800, height = 600)  # Specifica il nome del file e le dimensioni desiderate

plot(edf1, main="Empirical Distribution Function vs True Distribution Function", col="blue", lwd=2, ylim=c(0, 1))
lines(edf2, col="green", lwd=2)
lines(edf3, col="purple", lwd=2)
curve(pnorm(x), add=TRUE, col="red", lwd=2)
legend("topleft", legend=c("1000 samples", "100 samples", "50 samples","True Distribution Function"), col=c("blue", "green", "purple", "red"), lwd=2)

dev.off()  # Chiudi il dispositivo grafico
