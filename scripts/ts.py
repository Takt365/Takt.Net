#!/usr/bin/env python
# -*- coding: utf-8 -*-
"""
省市区数据爬取脚本
支持从多个数据源爬取中国省市区数据
"""
 
import os
import requests
import json
import csv
import time
from typing import List, Dict, Optional
from bs4 import BeautifulSoup
 
 
class ProvinceCityCrawler:
    """省市区数据爬取类"""
    
    def __init__(self):
        self.session = requests.Session()
        self.session.headers.update({
            'User-Agent': 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36'
        })
        self.data = []
    
    def crawl_from_mca(self, url: str = 'https://www.mca.gov.cn/mzsj/xzqh/2025/202401xzqh.html') -> List[Dict]:
        """
        从民政部网站爬取省市区数据
        URL: https://www.mca.gov.cn/mzsj/xzqh/2025/202401xzqh.html
        """
        # 页码缓存本地进行解析防止频繁抓取页面
        try:
            print(f"正在从民政部网站爬取省市区数据...")
            print(f"目标URL: {url}")
            # 缓存页面
            cache_file = 'mca_cache.html'
            if os.path.exists(cache_file):
                with open(cache_file, 'r', encoding='utf-8') as f:
                    soup = BeautifulSoup(f.read(), 'html.parser')
            else:
                response = self.session.get(url, timeout=30)
                response.encoding = 'utf-8'  # 民政部网站使用UTF-8编码
                soup = BeautifulSoup(response.text, 'html.parser')
                with open(cache_file, 'w', encoding='utf-8') as f:
                    f.write(soup.prettify())
            # 查找表格
            table = soup.find('table')
            if not table:
                # 尝试查找所有表格
                tables = soup.find_all('table')
                if tables:
                    table = tables[0]
                else:
                    print("未找到表格数据")
                    return []
            
            # 解析表格数据
            rows = table.find_all('tr')
            raw_data = []
            
            for row in rows:
                cols = row.find_all(['td', 'th'])
                if len(cols) < 2:
                    continue
                
                # 表格结构：第一列可能为空，第二列是代码，第三列是名称
                # 遍历所有列，找到代码和名称
                code = ''
                name = ''
                
                for i, col in enumerate(cols):
                    text = col.get_text().strip()
                    # 移除HTML中的空格、换行和特殊字符
                    text = text.replace('\n', '').replace('\r', '').replace('\t', '')
                    # 移除span标签中的空格
                    text = text.replace(' ', '')
                    
                    if not text:
                        continue
                    
                    # 检查是否是代码（纯数字，长度2-6位）
                    if text.isdigit() and 2 <= len(text) <= 6:
                        code = text
                        # 代码后面通常是名称，检查下一列
                        if i + 1 < len(cols):
                            next_text = cols[i + 1].get_text().strip()
                            next_text = next_text.replace('\n', '').replace('\r', '').replace('\t', '')
                            next_text = next_text.replace(' ', '')
                            # 如果下一列包含中文，就是名称
                            if next_text and any('\u4e00' <= char <= '\u9fff' for char in next_text):
                                name = next_text
                                break
                    # 检查是否是名称（包含中文，且不是纯数字）
                    elif any('\u4e00' <= char <= '\u9fff' for char in text) and not text.isdigit():
                        # 如果前面有代码，这个就是名称
                        if code:
                            name = text
                            break
                
                # 如果找到了代码但没找到名称，尝试从同一行的其他列查找
                if code and not name:
                    for col in cols:
                        text = col.get_text().strip()
                        text = text.replace('\n', '').replace('\r', '').replace('\t', '')
                        text = text.replace(' ', '')
                        if text and any('\u4e00' <= char <= '\u9fff' for char in text) and not text.isdigit():
                            name = text
                            break
                
                if code and name:
                    raw_data.append({
                        'code': code,
                        'name': name
                    })
            
            print(f"从表格中提取到 {len(raw_data)} 条原始数据")
            
            if not raw_data:
                return []
            
            # 根据民政部行政区划代码规则组织数据
            # 省级：6位代码，后4位为0000（如110000=北京市）
            # 市级：6位代码，后2位为00但前4位不为00（如110100=市辖区）
            # 区县级：6位代码（如110101=东城区）
            
            result = []
            province_dict = {}  # 存储省份信息 {province_code(2位): name}
            city_dict = {}      # 存储城市信息 {city_code(4位): (province_code, name)}
            
            # 第一遍：收集所有省市区数据
            for item in raw_data:
                code = item['code']
                name = item['name']
                
                # 处理6位代码
                if len(code) == 6:
                    if code.endswith('0000'):
                        # 省级：如110000
                        province_code = code[:2]
                        province_dict[province_code] = name
                    elif code.endswith('00') and not code.endswith('0000'):
                        # 市级：如110100
                        province_code = code[:2]
                        city_code = code[:4]
                        city_dict[city_code] = (province_code, name)
                    else:
                        # 区县级：如110101
                        province_code = code[:2]
                        city_code = code[:4]
                        
                        # 获取省份名称
                        province_name = province_dict.get(province_code, '')
                        
                        # 获取城市名称
                        if city_code in city_dict:
                            city_name = city_dict[city_code][1]
                        else:
                            # 如果城市代码不在字典中，尝试从原始数据中查找
                            city_name = ''
                            for raw_item in raw_data:
                                if raw_item['code'] == city_code or raw_item['code'] == city_code + '00':
                                    city_name = raw_item['name']
                                    break
                        
                        result.append({
                            'province_code': province_code,
                            'province_name': province_name,
                            'city_code': city_code,
                            'city_name': city_name,
                            'district_code': code,
                            'district_name': name
                        })
                # 处理4位代码（市级）
                elif len(code) == 4:
                    province_code = code[:2]
                    city_dict[code] = (province_code, name)
                # 处理2位代码（省级）
                elif len(code) == 2:
                    province_dict[code] = name
            
            # 第二遍：添加只有省市没有区县的数据
            for city_code, (province_code, city_name) in city_dict.items():
                province_name = province_dict.get(province_code, '')
                # 检查是否已经有该城市的区县数据
                has_district = any(
                    item['city_code'] == city_code 
                    for item in result
                )
                if not has_district:
                    result.append({
                        'province_code': province_code,
                        'province_name': province_name,
                        'city_code': city_code,
                        'city_name': city_name,
                        'district_code': '',
                        'district_name': ''
                    })
            
            # 第三遍：添加只有省没有市的数据（这种情况较少，如直辖市直接到区县）
            for province_code, province_name in province_dict.items():
                # 检查是否已经有该省的城市数据
                has_city = any(
                    item['province_code'] == province_code 
                    for item in result
                )
                if not has_city:
                    result.append({
                        'province_code': province_code,
                        'province_name': province_name,
                        'city_code': '',
                        'city_name': '',
                        'district_code': '',
                        'district_name': ''
                    })
            
            # 按省份代码、城市代码和区县代码排序
            result.sort(key=lambda x: (
                x['province_code'] or '00',
                x['city_code'] or '0000',
                x['district_code'] or '000000'
            ))
            
            self.data = result
            return result
            
        except Exception as e:
            print(f"从民政部网站爬取数据失败: {e}")
            import traceback
            traceback.print_exc()
            return []
    
    def save_to_json(self, filename: str = 'provinces_cities_districts.json'):
        """保存数据为JSON格式"""
        if not self.data:
            print("没有数据可保存")
            return
        
        with open(filename, 'w', encoding='utf-8') as f:
            json.dump(self.data, f, ensure_ascii=False, indent=2)
        print(f"数据已保存到 {filename}")
    
    def save_to_csv(self, filename: str = 'provinces_cities_districts.csv'):
        """保存数据为CSV格式"""
        if not self.data:
            print("没有数据可保存")
            return
        
        if not self.data:
            return
        
        fieldnames = ['province_code', 'province_name', 'city_code', 'city_name', 
                     'district_code', 'district_name']
        
        with open(filename, 'w', newline='', encoding='utf-8-sig') as f:
            writer = csv.DictWriter(f, fieldnames=fieldnames)
            writer.writeheader()
            writer.writerows(self.data)
        
        print(f"数据已保存到 {filename}")
    
    def save_to_txt(self, filename: str = 'provinces_cities_districts.txt'):
        """保存数据为文本格式"""
        if not self.data:
            print("没有数据可保存")
            return
        
        with open(filename, 'w', encoding='utf-8') as f:
            for item in self.data:
                line = f"{item['province_name']} - {item['city_name']} - {item['district_name']}\n"
                f.write(line)
        
        print(f"数据已保存到 {filename}")
    
    def print_statistics(self):
        """打印统计信息"""
        if not self.data:
            print("没有数据")
            return
        
        provinces = set(item['province_name'] for item in self.data)
        cities = set((item['province_name'], item['city_name']) for item in self.data)
        districts = len([item for item in self.data if item['district_name']])
        
        print("\n=== 数据统计 ===")
        print(f"省份数量: {len(provinces)}")
        print(f"城市数量: {len(cities)}")
        print(f"区县数量: {districts}")
        print(f"总记录数: {len(self.data)}")
 
 
def main():
    """主函数"""
    crawler = ProvinceCityCrawler()
    
    print("=" * 50)
    print("省市区数据爬取工具")
    print("=" * 50)
    
    # 从民政部网站爬取数据
    mca_url = 'https://www.mca.gov.cn/mzsj/xzqh/2025/202401xzqh.html'
    data = crawler.crawl_from_mca(mca_url)
    
    
    if data:
        # 打印统计信息
        crawler.print_statistics()
        
        # 保存数据
        print("\n正在保存数据...")
        crawler.save_to_json()
        crawler.save_to_csv()
        crawler.save_to_txt()
        
        print("\n爬取完成！")
    else:
        print("\n未能获取数据，请检查网络连接或数据源")
 
 
if __name__ == '__main__':
    main()
 