require('dotenv').config();
const path = require('path');
const merge = require('webpack-merge');
const webpack = require('webpack');

const common = require('./webpack.common.js');
const port = process.env.DEV_PORT;

const proxy = require('./proxy.json');

module.exports = merge(common, {
  mode: 'development',
  devtool: 'inline-source-map',
  plugins: [new webpack.HotModuleReplacementPlugin()],
  devServer: {
    port,
    proxy: proxy,
    open: true,
    https: false,
    compress: true,
    hot: true,
    contentBase: path.resolve(__dirname, 'dist'),
    watchContentBase: true,
    publicPath: '/',
    clientLogLevel: 'none',
    historyApiFallback: {
      // Paths with dots should still use the history fallback.
      // See https://github.com/facebook/create-react-app/issues/387.
      disableDotRule: true,
    },
  },
});
