const { resolve, join } = require('path');
const webpack = require('webpack');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const Dotenv = require('dotenv-webpack');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const CopyPlugin = require('copy-webpack-plugin');

const outputFolderPath = resolve(__dirname, 'dist');

module.exports = {
  context: resolve(__dirname, ''),
  entry: ['url-polyfill', join(__dirname, 'src', 'index.tsx')],
  output: {
    path: outputFolderPath,
    filename: 'app.[hash].js',
  },
  module: {
    rules: [
      // pre validate
      {
        test: /\.(ts|tsx)$/,
        enforce: 'pre',
        use: ['eslint-loader'],
        include: [resolve(__dirname, 'src')],
      },
      // load ts
      {
        test: /\.(ts|tsx)$/,
        exclude: /node_modules/,
        use: ['ts-loader'],
      },
      // load css
      {
        test: /\.css$/,
        use: [
          {
            loader: MiniCssExtractPlugin.loader,
            options: {
              // only enable hot in development
              hmr: process.env.NODE_ENV === 'development',
              // if hmr does not work, this is a forceful method.
              reloadAll: true,
            },
          },
          'css-loader',
          'postcss-loader',
        ],
      },
      // load less files
      {
        test: /\.less$/,
        exclude: /\.module\.less$/,
        use: [
          {
            loader: MiniCssExtractPlugin.loader,
            options: {
              // only enable hot in development
              hmr: process.env.NODE_ENV === 'development',
              // if hmr does not work, this is a forceful method.
              reloadAll: true,
            },
          },
          {
            loader: 'css-loader',
            options: {
              importLoaders: 2,
              sourceMap: true,
            },
          },
          {
            loader: 'postcss-loader',
            options: {
              sourceMap: true,
            },
          },
          {
            loader: 'less-loader',
            options: {
              lessOptions: {
                javascriptEnabled: true,
              },
            },
          },
        ],
      },
      // load less modules
      {
        test: /\.module\.less$/,
        use: [
          {
            loader: MiniCssExtractPlugin.loader,
            options: {
              // only enable hot in development
              hmr: process.env.NODE_ENV === 'development',
              // if hmr does not work, this is a forceful method.
              reloadAll: true,
            },
          },
          'css-modules-typescript-loader',
          {
            loader: 'css-loader',
            options: {
              importLoaders: 2,
              sourceMap: true,
              modules: true,
              esModule: true,
            },
          },
          {
            loader: 'postcss-loader',
            options: {
              sourceMap: true,
            },
          },
          {
            loader: 'less-loader',
            options: {
              lessOptions: {
                javascriptEnabled: true,
              },
            },
          },
        ],
      },
      // load assets
      {
        test: /\.(eot|otf|ttf|woff|woff2)$/,
        use: 'file-loader',
      },
      {
        test: /\.svg$/,
        use: [
          {
            loader: 'svg-url-loader',
            options: {
              // Inline files smaller than 10 kB
              limit: 10 * 1024,
              noquotes: true,
            },
          },
        ],
      },
      {
        test: /\.(jpg|png|gif)$/,
        use: [
          {
            loader: 'url-loader',
            options: {
              // Inline files smaller than 10 kB
              limit: 10 * 1024,
            },
          },
        ],
      },
    ],
  },
  plugins: [
    new Dotenv({ systemvars: true }),
    new webpack.EnvironmentPlugin(['NODE_ENV']),
    new MiniCssExtractPlugin({ filename: 'style.[hash].css' }),
    new HtmlWebpackPlugin({
      title: 'App template',
      template: join(__dirname, 'public', 'template.ejs'),
    }),
    new CopyPlugin([{ from: 'public', ignore: ['template.ejs'] }]),
  ],
  performance: {
    hints: false,
  },
  resolve: {
    modules: ['node_modules', 'src'],
    extensions: ['.js', '.jsx', '.json', '.ts', '.tsx', '.less'],
  },
};
