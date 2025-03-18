"use strict";
/**
 * japanpost-zipcode
 *
 * @see https://www.npmjs.com/package/japanpost-zipcode
 */
var _a;
Object.defineProperty(exports, "__esModule", { value: true });
exports.KenAll = void 0;
const fs_1 = require("fs");
const iconv = require("iconv-cp932");
const JSZip = require("jszip");
const node_fetch_1 = require("node-fetch");
const os = require("os");
const removeKanaSuffix = new RegExp("(ｲｶﾆｹｲｻｲｶﾞﾅｲﾊﾞｱｲ|.*ﾉﾂｷﾞﾆﾊﾞﾝﾁｶﾞｸﾙﾊﾞｱｲ|\(.*?\))$");
const removeTextSuffix = new RegExp("(以下に掲載がない場合|.*に番地がくる場合|（.*?）)$");
const removeChiwariSuffix = new RegExp("第?[０-９]*地割([、～].*?[０-９]*地割)?$");
const removeIchienSuffix = new RegExp("(.+[市区町村])(一円)$");
/**
 * @see https://www.post.japanpost.jp/zipcode/dl/readme.html
 */
const defaultOptions = {
    logger: undefined,
    url: "https://www.post.japanpost.jp/zipcode/dl/kogaki/zip/ken_all.zip",
    zip: "ken_all.zip",
    csv: "KEN_ALL.CSV",
    json: "ken_all.json",
    tmpDir: (_a = os.tmpdir()) === null || _a === void 0 ? void 0 : _a.replace(/\/?$/, "/"),
};
/**
 * 郵便番号データダウンロード（読み仮名データの促音・拗音を小書きで表記するもの）（全国一括）
 */
class KenAll {
    constructor(options) {
        const that = this;
        for (const key in defaultOptions) {
            const k = key;
            that[k] = options && options[k] || defaultOptions[k];
        }
    }
    tmpZip() {
        return this.tmpDir + "ken_all.zip";
    }
    tmpJson() {
        return this.tmpDir + "ken_all.json";
    }
    /**
     * console.warn
     */
    debug(message) {
        if (this.logger)
            this.logger.warn(message);
    }
    /**
     * fetch ZIP file
     */
    async fetchZip() {
        this.debug("loading: " + this.url);
        const res = await (0, node_fetch_1.default)(this.url);
        return Buffer.from(await res.arrayBuffer());
    }
    /**
     * Open ZIP file
     */
    async openZipCSV() {
        const { _file } = this;
        if (_file)
            return _file;
        const zipPath = this.tmpZip();
        try {
            await fs_1.promises.access(zipPath);
        }
        catch (e) {
            const data = await this.fetchZip();
            this.debug("writing: " + zipPath);
            await fs_1.promises.writeFile(zipPath, data);
        }
        this.debug("reading: " + zipPath);
        const data = await fs_1.promises.readFile(zipPath);
        if (!data)
            return Promise.reject(`empty: ${zipPath}`);
        const zip = await JSZip.loadAsync(data);
        return this._file = zip.file(this.csv);
    }
    /**
     * extract CSV file from ZIP file
     */
    async extractCSV() {
        const file = await this.openZipCSV();
        const ab = await file.async("arraybuffer");
        const buffer = Buffer.from(ab);
        return iconv.decode(buffer);
    }
    /**
     * get the last modified time of CSV in ZIP
     */
    async modifiedAt() {
        const file = await this.openZipCSV();
        const modified = file.date;
        this.debug("modified: " + JSON.stringify(modified));
        return modified;
    }
    /**
     * parse raw CSV file
     */
    async parseRawCSV() {
        const data = await this.extractCSV();
        const rows = data.split(/\r?\n/)
            .filter(line => line)
            .map(line => line.split(",")
            .map(col => col.replace(/^"(.*)"/, "$1")));
        const index = {};
        return rows.filter(row => {
            const zip = row[2 /* C.郵便番号 */];
            const prev = index[zip];
            // same city
            if (prev && prev[0] === row[0]) {
                // continued line
                const open = prev[8 /* C.町域名 */].split("（").length;
                const close = prev[8 /* C.町域名 */].split("）").length;
                if (open > close) {
                    prev[5 /* C.町域名カナ */] += row[5 /* C.町域名カナ */];
                    prev[8 /* C.町域名 */] += row[8 /* C.町域名 */];
                    return false;
                }
            }
            index[zip] = row;
            return true;
        });
    }
    /**
     * load CSV file from cache when available
     */
    async readCachedCSV() {
        const jsonPath = this.tmpJson();
        try {
            await fs_1.promises.access(jsonPath);
        }
        catch (e) {
            const data = await this.parseRawCSV();
            this.debug("writing: " + jsonPath);
            const json = JSON.stringify(data).replace(/],/g, "],\n");
            await fs_1.promises.writeFile(jsonPath, json);
        }
        this.debug("reading: " + jsonPath);
        const data = await fs_1.promises.readFile(jsonPath);
        return JSON.parse(data + "");
    }
    /**
     * normalize
     */
    normalize(row) {
        if (row[5 /* C.町域名カナ */]) {
            row[5 /* C.町域名カナ */] = row[5 /* C.町域名カナ */].replace(removeKanaSuffix, "");
        }
        if (row[8 /* C.町域名 */]) {
            row[8 /* C.町域名 */] = row[8 /* C.町域名 */].replace(removeTextSuffix, "");
            row[8 /* C.町域名 */] = row[8 /* C.町域名 */].replace(removeChiwariSuffix, "");
            row[8 /* C.町域名 */] = row[8 /* C.町域名 */].replace(removeIchienSuffix, "");
        }
    }
    /**
     * parse CSV file
     */
    async readAll() {
        const rows = await this.readCachedCSV();
        rows.forEach(row => this.normalize(row));
        return rows;
    }
    /**
     * remove temporary files
     */
    async clean() {
        const removeFile = async (path) => {
            try {
                await fs_1.promises.access(path);
                this.debug("removing: " + path);
                await fs_1.promises.rm(path);
            }
            catch (e) {
                //
            }
        };
        await removeFile(this.tmpZip());
        await removeFile(this.tmpJson());
    }
    /**
     * parse CSV file
     */
    static async readAll(options) {
        return new KenAll(options).readAll();
    }
}
exports.KenAll = KenAll;
