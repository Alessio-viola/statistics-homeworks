# Imposta il seme per la riproducibilità
set.seed(123)

# Numero di osservazioni
n <- 1000

# Tasso (lambda) della distribuzione di Poisson
lambda <- 5

# Genera dati distribuiti secondo una Poisson
dati_poisson <- rpois(n, lambda)

# Istogramma dei dati
hist(dati_poisson, breaks = seq(-0.5, max(dati_poisson) + 0.5, by = 1), col = "lightblue", main = "Distribuzione di Poisson", xlab = "Numero di eventi", ylab = "Frequenza")

# Calcola le probabilità della distribuzione di Poisson
x <- 0:max(dati_poisson)
prob_poisson <- dpois(x, lambda)

# Traccia la curva caratteristica di Poisson
lines(x, prob_poisson * n, col = "red", lwd = 2)

# Aggiungi una legenda
legend("topright", legend = c("Dati Poisson", "Poisson Teorica"), col = c("lightblue", "red"), lwd = 2)

# Salva l'immagine come file JPEG
jpeg("distribuzione_poisson.jpeg", quality = 100)
