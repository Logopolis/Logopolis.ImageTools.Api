const port = 53281

const express = require("express")
const path = require('path')

const app = express()
const router = express.Router()

router.get('/',function(req,res){
    console.log("Connected")
  res.sendFile(path.join(__dirname+'/form.html'))
});

app.use('/', router)
app.listen(process.env.port || port)