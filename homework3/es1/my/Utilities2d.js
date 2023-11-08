class My2dUtilities {
    constructor(canvasId) {
      this.canvas = document.getElementById(canvasId);
      this.context = this.canvas.getContext('2d');
    }
  
    clearCanvas() {
      this.context.clearRect(0, 0, this.canvas.width, this.canvas.height);
    }
  
    drawRectangle(x, y, width, height, color) {
      this.context.fillStyle = color;
      this.context.fillRect(x, y, width, height);
    }
  
    drawCircle(x, y, radius, color) {
      this.context.fillStyle = color;
      this.context.beginPath();
      this.context.arc(x, y, radius, 0, 2 * Math.PI);
      this.context.fill();
    }
  
    drawText(text, x, y, font, color) {
      this.context.fillStyle = color;
      this.context.font = font;
      this.context.fillText(text, x, y);
    }
  
    createCartesianGraph(xAxisLabel, yAxisLabel, xMin, xMax, yMin, yMax) {
        const ctx = this.context;
        const canvasWidth = this.canvas.width;
        const canvasHeight = this.canvas.height;
        const xAxisSpacing = 20; // Spaziatura per le ascisse
        const yAxisSpacing = 20; // Spaziatura per le ordinate

        // Disegna assi
        ctx.beginPath();
        ctx.moveTo(0, canvasHeight / 2);
        ctx.lineTo(canvasWidth, canvasHeight / 2);
        ctx.moveTo(canvasWidth / 2, 0);
        ctx.lineTo(canvasWidth / 2, canvasHeight);
        ctx.strokeStyle = 'black';
        ctx.lineWidth = 2;
        ctx.stroke();

        // Etichette sugli assi
        ctx.font = '12px Arial';
        ctx.fillStyle = 'black';
        ctx.textAlign = 'center';
        ctx.fillText(xAxisLabel, canvasWidth - 10, canvasHeight / 2 - 10);
        ctx.fillText(yAxisLabel, canvasWidth / 2 + 10, 10);

        // Calcola scaling
        const xScale = canvasWidth / (xMax - xMin);
        const yScale = canvasHeight / (yMax - yMin);

        // Disegna numeri sulle ascisse
        for (let x = xMin; x <= xMax; x++) {
            const xCoord = x * xScale + canvasWidth / 2;
            ctx.beginPath();
            ctx.moveTo(xCoord, canvasHeight / 2 - xAxisSpacing / 2);
            ctx.lineTo(xCoord, canvasHeight / 2 + xAxisSpacing / 2);
            ctx.strokeStyle = 'black';
            ctx.lineWidth = 2;
            ctx.stroke();
            ctx.fillText(x.toString(), xCoord, canvasHeight / 2 + xAxisSpacing);
        }

        // Disegna numeri sulle ordinate
        for (let y = yMin; y <= yMax; y++) {
            const yCoord = canvasHeight / 2 - y * yScale;
            ctx.beginPath();
            ctx.moveTo(canvasWidth / 2 - yAxisSpacing / 2, yCoord);
            ctx.lineTo(canvasWidth / 2 + yAxisSpacing / 2, yCoord);
            ctx.strokeStyle = 'black';
            ctx.lineWidth = 2;
            ctx.stroke();
            ctx.fillText(y.toString(), canvasWidth / 2 - yAxisSpacing, yCoord);
        }

        return { xScale, yScale, xMin, yMin };
    }
  }

  class ResizableRectangle {
    constructor(rectangleId, handleId) {
        this.rectangle = document.getElementById(rectangleId);
        this.handle = document.getElementById(handleId);

        this.isResizing = false;
        this.originalX = 0;
        this.originalY = 0;
        this.originalWidth = 0;
        this.originalHeight = 0;

        this.handle.addEventListener('mousedown', this.startResize.bind(this));
        document.addEventListener('mousemove', this.resize.bind(this));
        document.addEventListener('mouseup', this.stopResize.bind(this));
    }

    startResize(e) {
        e.preventDefault();
        this.isResizing = true;
        this.originalX = e.clientX;
        this.originalY = e.clientY;
        this.originalWidth = parseFloat(getComputedStyle(this.rectangle).width);
        this.originalHeight = parseFloat(getComputedStyle(this.rectangle).height);
    }

    resize(e) {
        if (!this.isResizing) return;

        const newWidth = this.originalWidth + (e.clientX - this.originalX);
        const newHeight = this.originalHeight + (e.clientY - this.originalY);

        if (newWidth >= 50 && newHeight >= 50) {
            this.rectangle.style.width = newWidth + 'px';
            this.rectangle.style.height = newHeight + 'px';
        }
    }

    stopResize() {
        this.isResizing = false;
    }
}

const resizableRect = new ResizableRectangle('resizableRect', 'resizeHandle');